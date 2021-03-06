﻿#region netDxf, Copyright(C) 2013 Daniel Carvajal, Licensed under LGPL.

//                        netDxf library
// Copyright (C) 2013 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;
using netDxf.Blocks;
using netDxf.Collections;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
using netDxf.Tables;
using Attribute = netDxf.Entities.Attribute;

namespace netDxf
{
    /// <summary>
    /// Represents a document to read and write dxf files.
    /// </summary>
    public class DxfDocument
    {

        #region private fields

        private string name;
        // keeps track of the number of handles generated
        internal int NumHandles;
        // keeps track of the dimension blocks generated
        internal int DimensionBlocksGenerated;
        // keeps track of the group names generated (this groups have the isUnnamed bool set to true)
        internal int GroupNamesGenerated;
        // during the save process new handles are needed for the table sections, this number should be enough
        private const int ReservedHandles = 10;

        #region header

        private List<string> comments;
        private HeaderVariables drawingVariables;

        #endregion

        #region tables

        private readonly new List<Viewport> viewports;

        private ApplicationRegistries appRegistries;
        private Layers layers;
        private LineTypes lineTypes;
        private TextStyles textStyles;
        private DimensionStyles dimStyles;
        private MLineStyles mlineStyles;
        private UCSs ucss;
        private BlockRecords blocks;
        private ImageDefinitions imageDefs;
        private Groups groups;

        #endregion

        #region entities

        //entity objects added to the document (key: handle, value: entity). This dictionary also includes entities that are part of a block.
        private Dictionary<string, EntityObject> addedEntity;
        private List<Arc> arcs;
        private List<Circle> circles;
        private List<Dimension> dimensions; 
        private List<Ellipse> ellipses;
        private List<Solid> solids;
        private List<Face3d> faces3d;
        private List<Insert> inserts;
        private List<Line> lines;
        private List<Point> points;
        private List<PolyfaceMesh> polyfaceMeshes;
        private List<LwPolyline> lwPolylines;
        private List<Polyline> polylines;
        private List<Text> texts;
        private List<MText> mTexts;
        private List<Hatch> hatches;
        private List<Spline> splines;
        private List<Image> images;
        private List<MLine> mLines;
        private List<Ray> rays;
        private List<XLine> xlines;

        #endregion

        #region objects

        private RasterVariables rasterVariables;

        #endregion

        #endregion

        #region constructor

        /// <summary>
        /// Initalizes a new instance of the <c>DxfDocument</c> class.
        /// </summary>
        /// <remarks>The default <see cref="HeaderVariables">drawing variables</see> of the document will be used.</remarks>
        public DxfDocument()
            : this(new HeaderVariables())
        {
        }

        /// <summary>
        /// Initalizes a new instance of the <c>DxfDocument</c> class.
        /// </summary>
        /// <param name="version">AutoCAD drawing database version number.</param>
        public DxfDocument(DxfVersion version)
            : this(new HeaderVariables{AcadVer = version})
        {
        }

        /// <summary>
        /// Initalizes a new instance of the <c>DxfDocument</c> class.
        /// </summary>
        /// <param name="drawingVariables"><see cref="HeaderVariables">Drawing variables</see> of the document.</param>
        public DxfDocument(HeaderVariables drawingVariables)
        {

            this.comments = new List<string> {"Dxf file generated by netDxf http://netdxf.codeplex.com, Copyright(C) 2013 Daniel Carvajal, Licensed under LGPL"};
            this.drawingVariables = drawingVariables;

            this.NumHandles = 1;
            this.DimensionBlocksGenerated = 0;
            this.GroupNamesGenerated = 0;
            this.addedEntity = new Dictionary<string, EntityObject>(); // keeps track of the added object to avoid duplicates

            // tables
            this.viewports = new List<Viewport>();
            this.appRegistries = new ApplicationRegistries(this);
            this.layers = new Layers(this);
            this.lineTypes = new LineTypes(this);
            this.textStyles = new TextStyles(this);
            this.dimStyles = new DimensionStyles(this);
            this.mlineStyles = new MLineStyles(this);
            this.ucss = new UCSs(this);
            this.blocks = new BlockRecords(this);
            this.imageDefs = new ImageDefinitions(this);
            this.groups = new Groups(this);
            
            this.AddDefaultObjects();

            // entities lists
            this.arcs = new List<Arc>();
            this.ellipses = new List<Ellipse>();
            this.dimensions = new List<Dimension>();
            this.faces3d = new List<Face3d>();
            this.solids = new List<Solid>();
            this.inserts = new List<Insert>();
            this.lwPolylines = new List<LwPolyline>();
            this.polylines = new List<Polyline>();
            this.polyfaceMeshes = new List<PolyfaceMesh>();
            this.lines = new List<Line>();
            this.circles = new List<Circle>();
            this.points = new List<Point>();
            this.texts = new List<Text>();
            this.mTexts = new List<MText>();
            this.hatches = new List<Hatch>();
            this.splines = new List<Spline>();
            this.images = new List<Image>();
            this.mLines = new List<MLine>();
            this.rays = new List<Ray>();
            this.xlines = new List<XLine>();
        }

        #endregion

        #region public properties

        #region header

        /// <summary>
        /// Gets or sets the name of the document, once a file is saved or loaded this field is equals the file name without extension.
        /// </summary>
        public List<string> Comments
        {
            get { return this.comments; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.comments = value;
            }
        }

        /// <summary>
        /// Gets the dxf <see cref="HeaderVariables">drawing variables</see>.
        /// </summary>
        public HeaderVariables DrawingVariables
        {
            get { return this.drawingVariables; }
        }

        /// <summary>
        /// Gets or sets the name of the document, once a file is saved or loaded this field is equals the file name without extension.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        #endregion

        #region  public table properties

        /// <summary>
        /// Gets the <see cref="ApplicationRegistries">application registries</see> collection.
        /// </summary>
        public ApplicationRegistries ApplicationRegistries
        {
            get { return this.appRegistries; }
        }

        /// <summary>
        /// Gets the <see cref="Layers">layer</see> collection.
        /// </summary>
        public Layers Layers
        {
            get { return this.layers; }
        }

        /// <summary>
        /// Gets the <see cref="LineTypes">linetype</see> collection.
        /// </summary>
        public LineTypes LineTypes
        {
            get { return this.lineTypes; }
        }

        /// <summary>
        /// Gets the <see cref="TextStyles">text style</see> collection.
        /// </summary>
        public TextStyles TextStyles
        {
            get { return this.textStyles; }
        }

        /// <summary>
        /// Gets the <see cref="DimensionStyles">dimension style</see> collection.
        /// </summary>
        public DimensionStyles DimensionStyles
        {
            get { return this.dimStyles; }
        }

        /// <summary>
        /// Gets the <see cref="MLineStyles">MLine styles</see> collection.
        /// </summary>
        public MLineStyles MlineStyles
        {
            get { return this.mlineStyles; }
        }

        /// <summary>
        /// Gets the <see cref="UCSs">User coordinate system</see> collection.
        /// </summary>
        public UCSs UCSs
        {
            get { return this.ucss; }
        }

        /// <summary>
        /// Gets the <see cref="Blocks">block</see> collection.
        /// </summary>
        public BlockRecords Blocks
        {
            get { return this.blocks; }
        }

        /// <summary>
        /// Gets the <see cref="ImageDefinitions">image definitions</see> collection.
        /// </summary>
        public ImageDefinitions ImageDefinitions
        {
            get { return this.imageDefs; }
        }

        /// <summary>
        /// Gets the <see cref="Groups">groups</see> collection.
        /// </summary>
        public Groups Groups
        {
            get { return this.groups; }
        }

        #endregion

        #region public entities properties

        /// <summary>
        /// Gets the <see cref="Arc">arcs</see> list.
        /// </summary>
        public ReadOnlyCollection<Arc> Arcs
        {
            get { return this.arcs.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Ellipse">ellipses</see> list.
        /// </summary>
        public ReadOnlyCollection<Ellipse> Ellipses
        {
            get { return this.ellipses.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Circle">circles</see> list.
        /// </summary>
        public ReadOnlyCollection<Circle> Circles
        {
            get { return this.circles.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Face3d">3d faces</see> list.
        /// </summary>
        public ReadOnlyCollection<Face3d> Faces3d
        {
            get { return this.faces3d.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Solid">solids</see> list.
        /// </summary>
        public ReadOnlyCollection<Solid> Solids
        {
            get { return this.solids.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Insert">inserts</see> list.
        /// </summary>
        public ReadOnlyCollection<Insert> Inserts
        {
            get { return this.inserts.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Line">lines</see> list.
        /// </summary>
        public ReadOnlyCollection<Line> Lines
        {
            get { return this.lines.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Polyline">polylines</see> list.
        /// </summary>
        public ReadOnlyCollection<Polyline> Polylines
        {
            get { return this.polylines.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="LwPolyline">light weight polylines</see> list.
        /// </summary>
        public ReadOnlyCollection<LwPolyline> LwPolylines
        {
            get { return this.lwPolylines.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="PolyfaceMeshes">polyface meshes</see> list.
        /// </summary>
        public ReadOnlyCollection<PolyfaceMesh> PolyfaceMeshes
        {
            get { return this.polyfaceMeshes.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Point">points</see> list.
        /// </summary>
        public ReadOnlyCollection<Point> Points
        {
            get { return this.points.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Text">texts</see> list.
        /// </summary>
        public ReadOnlyCollection<Text> Texts
        {
            get { return this.texts.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="MText">multiline texts</see> list.
        /// </summary>
        public ReadOnlyCollection<MText> MTexts
        {
            get { return this.mTexts.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Hatch">hatches</see> list.
        /// </summary>
        public ReadOnlyCollection<Hatch> Hatches
        {
            get { return this.hatches.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Image">images</see> list.
        /// </summary>
        public ReadOnlyCollection<Image> Images
        {
            get { return this.images.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="MLines">multilines</see> list.
        /// </summary>
        public ReadOnlyCollection<MLine> MLines
        {
            get { return this.mLines.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Dimension">dimensions</see> list.
        /// </summary>
        public ReadOnlyCollection<Dimension> Dimensions
        {
            get { return this.dimensions.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Splines">splines</see> list.
        /// </summary>
        public ReadOnlyCollection<Spline> Splines
        {
            get { return this.splines.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="Ray">rays</see> list.
        /// </summary>
        public ReadOnlyCollection<Ray> Rays
        {
            get { return this.rays.AsReadOnly(); }
        }


        /// <summary>
        /// Gets the <see cref="XLine">xlines</see> list.
        /// </summary>
        public ReadOnlyCollection<XLine> XLines
        {
            get { return this.xlines.AsReadOnly(); }
        }

        #endregion

        #region public object properties

        /// <summary>
        /// Gets the <see cref="RasterVariables">raster variables</see> applied to image entities.
        /// </summary>
        public RasterVariables RasterVariables
        {
            get { return this.rasterVariables; }
        }

        #endregion

        #endregion

        #region public entity methods

        /// <summary>
        /// Gets an entity provided its handle.
        /// </summary>
        /// <param name="handle">Entity object handle.</param>
        /// <returns>The entity associated with the provided handle, null if it is not found.</returns>
        /// <remarks>This method will also return entities that are part of a block definition.</remarks>
        public EntityObject GetEntityByHandle(string handle)
        {
            return this.addedEntity[handle];
        }

        /// <summary>
        /// Adds a list of <see cref="EntityObject">entities</see> to the document.
        /// </summary>
        /// <param name="entities">A list of <see cref="EntityObject">entities</see> to add to the document.</param>
        /// <remarks>
        /// <para>
        /// Once an entity has been added to the dxf document, it should not be modified. A unique handle identifier is assigned to every entity.
        /// </para>
        /// <para>
        /// This is specially true in the case of dimensions. The block that represents the drawing of the dimension is built
        /// when it is added to the document. If a property is modified once it has been added this modifications will not be 
        /// reflected in the saved dxf file.
        /// </para>
        /// </remarks>
        public void AddEntity(IEnumerable<EntityObject> entities)
        {
            foreach (EntityObject entity in entities)
            {
                this.AddEntity(entity);
            }
        }

        /// <summary>
        /// Adds an <see cref="EntityObject">entity</see> to the document.
        /// </summary>
        /// <param name="entity">An <see cref="EntityObject">entity</see> to add to the document.</param>
        /// <remarks>
        /// <returns>True if the entity has been added to the document, false otherwise.</returns>
        /// <para>
        /// Once an entity has been added to the dxf document a unique handle identifier (hexadecimal number) is assigned to them.
        /// </para>
        /// <para>
        /// The entities should not be modified. This is specially true in the case of dimensions. The block that represents the drawing of the dimension is built
        /// when it is added to the document. If a property is modified once it has been added this modifications will not be reflected in the saved dxf file.
        /// </para>
        /// </remarks>
        public bool AddEntity(EntityObject entity)
        {
            return this.AddEntity(entity, false);
        }

        /// <summary>
        /// Removes a list of <see cref="EntityObject">entities</see> from the document.
        /// </summary>
        /// <param name="entities">A list of <see cref="EntityObject">entities</see> to remove from the document.</param>
        /// <remarks>
        /// This function will not remove other tables objects that might be not in use as result from the elimination of the entity.<br />
        /// This includes empity layers, blocks not referenced anymore, line types, text styles, dimension styles, and application registries.<br />
        /// Entities that are part of a block definition will not be removed.
        /// </remarks>
        public void RemoveEntity(IEnumerable<EntityObject> entities)
        {
            foreach (EntityObject entity in entities)
            {
                this.RemoveEntity(entity, false);
            }
        }

        /// <summary>
        /// Removes an <see cref="EntityObject">entity</see> from the document.
        /// </summary>
        /// <param name="entity">The <see cref="EntityObject">entity</see> to remove from the document.</param>
        /// <returns>True if item is successfully removed; otherwise, false. This method also returns false if item was not found.</returns>
        /// <remarks>
        /// This function will not remove other tables objects that might be not in use as result from the elimination of the entity.<br />
        /// This includes empity layers, blocks not referenced anymore, line types, text styles, dimension styles, multiline styles, groups, and application registries.<br />
        /// Entities that are part of a block definition will not be removed.
        /// </remarks>
        public bool RemoveEntity(EntityObject entity)
        {
            return this.RemoveEntity(entity, false);
        }

        #endregion

        #region public methods

        /// <summary>
        /// Loads a dxf file.
        /// </summary>
        /// <param name="file">File name.</param>
        /// <returns>Returns a DxfDocument. It will return null if the file has not been able to load.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <remarks>
        /// Loading dxf files prior to AutoCad 2000 is not supported.<br />
        /// The Load method will still raise an exception if they are unable to create the FileStream.<br />
        /// On Debug mode it will raise any exception that migh occur during the whole process.
        /// </remarks>
        public static DxfDocument Load(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("File " + fileInfo.FullName + " not found.", fileInfo.FullName);

            Stream stream;
            try
            {
                stream = File.OpenRead(file);
            }
            catch (Exception ex)
            {
                throw new IOException("Error trying to open the file " + fileInfo.FullName + " for reading.", ex);
            }

            // In dxf files the decimal point is always a dot. We have to make sure that this doesn't interfere with the system configuration.
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

#if DEBUG
            DxfDocument document = InternalLoad(stream);
            stream.Close();
            Thread.CurrentThread.CurrentCulture = cultureInfo;
#else
            DxfDocument document;
            try
            {
                 document = InternalLoad(stream);
            }
            catch
            {
                return null;
            }
            finally
            {
                stream.Close();
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }

#endif
            document.name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
            return document;
        }

        /// <summary>
        /// Loads a dxf file.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <returns>Returns a DxfDocument. It will return null if the file has not been able to load.</returns>
        /// <remarks>
        /// Loading dxf files prior to AutoCad 2000 is not supported.<br />
        /// On Debug mode it will raise any exception that might occur during the whole process.
        /// </remarks>
        public static DxfDocument Load(Stream stream)
        {
            // In dxf files the decimal point is always a dot. We have to make sure that this doesn't interfere with the system configuration.
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

#if DEBUG
            DxfDocument document = InternalLoad(stream);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
#else
            DxfDocument document;
            try
            {
                 document = InternalLoad(stream);
            }
            catch
            {
                return null;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }

#endif
            return document;
        }

        /// <summary>
        /// Saves the database of the actual DxfDocument to a dxf file.
        /// </summary>
        /// <param name="file">File name.</param>
        /// <returns>Return true if the file has been succesfully save, false otherwise.</returns>
        /// <exception cref="IOException"></exception>
        /// <remarks>
        /// If the file already exists it will be overwritten.<br />
        /// The Save method will still raise an exception if they are unable to create the FileStream.<br />
        /// On Debug mode they will raise any exception that migh occur during the whole process.
        /// </remarks>
        public bool Save(string file)
        {

            FileInfo fileInfo = new FileInfo(file);
            this.name = Path.GetFileNameWithoutExtension(fileInfo.FullName);

            // In dxf files the decimal point is always a dot. We have to make sure that this doesn't interfere with the system configuration.
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Stream stream;
            try
            {
                stream = File.Create(file);
            }
            catch (Exception ex)
            {
                throw new IOException("Error trying to create the file " + fileInfo.FullName + " for writing.", ex);
            }

#if DEBUG
            this.InternalSave(stream);
            stream.Close();
            Thread.CurrentThread.CurrentCulture = cultureInfo;
#else
            try
            {
                InternalSave(stream);
            }
            catch
            {
                return false;
            }
            finally
            {
                stream.Close();
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
                
#endif
            return true;
        }

        /// <summary>
        /// Saves the database of the actual DxfDocument to a stream.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <returns>Return true if the stream has been succesfully saved, false otherwise.</returns>
        /// <remarks>
        /// On Debug mode it will raise any exception that might occur during the whole process.
        /// </remarks>
        public bool Save(Stream stream)
        {
            // In dxf files the decimal point is always a dot. We have to make sure that this doesn't interfere with the system configuration.
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

#if DEBUG
            this.InternalSave(stream);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
#else
            try
            {
                InternalSave(stream);
            }
            catch
            {
                return false;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
                
#endif
            return true;
        }

        /// <summary>
        /// Checks the AutoCAD dxf file database version.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>String that represents the dxf file version.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        public static DxfVersion CheckDxfFileVersion(Stream stream)
        {
            string value = DxfReader.CheckHeaderVariable(stream, HeaderVariableCode.AcadVer);
            return (DxfVersion) StringEnum.Parse(typeof (DxfVersion), value);
        }

        /// <summary>
        /// Checks the AutoCAD dxf file database version.
        /// </summary>
        /// <param name="file">File name.</param>
        /// <returns>String that represents the dxf file version.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        public static DxfVersion CheckDxfFileVersion(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("File " + fileInfo.FullName + " not found.", fileInfo.FullName);

            Stream stream;
            try
            {
                stream = File.OpenRead(file);
            }
            catch (Exception ex)
            {
                throw new IOException("Error trying to open the file " + fileInfo.FullName + " for reading.", ex);
            }

            string value;
            try
            {
                value = DxfReader.CheckHeaderVariable(stream, HeaderVariableCode.AcadVer);
            }
            catch
            {
                return DxfVersion.Unknown;
            }
            finally
            {
                stream.Close();
            }
            return (DxfVersion)StringEnum.Parse(typeof(DxfVersion), value);
        }

        #endregion

        #region private methods

        internal bool AddEntity(EntityObject entity, bool isBlockEntity)
        {
            // no null entities allowed
            if (entity == null)
                throw new ArgumentNullException("entity", "The entity cannot be null.");

            if (entity.Handle != null)
            {
                // check if the entity handle has been assigned
                if (this.addedEntity.ContainsKey(entity.Handle))
                {
                    // if the handle is equal the entity might come from another document, check if it is exactly the same object
                    EntityObject existing = this.addedEntity[entity.Handle];
                    // if the entity is already in the document return false, do not add it again
                    if (existing.Equals(entity))
                        return false;
                }
            }

            this.NumHandles = entity.AsignHandle(this.NumHandles);
            this.addedEntity.Add(entity.Handle, entity);

            foreach (string appReg in entity.XData.Keys)
            {
                entity.XData[appReg].ApplicationRegistry = this.appRegistries.Add(entity.XData[appReg].ApplicationRegistry);
                this.appRegistries.References[appReg].Add(entity);
            }

            entity.Layer = this.layers.Add(entity.Layer);
            this.layers.References[entity.Layer.Name].Add(entity);

            entity.LineType = this.lineTypes.Add(entity.LineType);
            this.lineTypes.References[entity.LineType.Name].Add(entity);

            // the entities that are part of a block do not belong to any of the entities lists but to the block definition.
            switch (entity.Type)
            {
                case EntityType.Arc:
                    if(!isBlockEntity) this.arcs.Add((Arc)entity);
                    break;
                case EntityType.Circle:
                    if (!isBlockEntity) this.circles.Add((Circle)entity);
                    break;
                case EntityType.Dimension:
                    if (!isBlockEntity) this.dimensions.Add((Dimension)entity);
                    ((Dimension)entity).Style = this.dimStyles.Add(((Dimension)entity).Style);
                    this.dimStyles.References[((Dimension)entity).Style.Name].Add(entity);

                    // create the block that represent the dimension drawing
                    Block dimBlock = ((Dimension)entity).BuildBlock("*D" + ++this.DimensionBlocksGenerated);
                    if (this.blocks.Contains(dimBlock.Name))
                        throw new ArgumentException("The list already contains the block: " + dimBlock.Name + ". The block names that start with *D are reserverd for dimensions");
                    dimBlock.TypeFlags = BlockTypeFlags.AnonymousBlock;
                    // add the dimension block to the document
                    this.blocks.Add(dimBlock);
                    this.blocks.References[dimBlock.Name].Add(entity);
                    break;
                case EntityType.Ellipse:
                    if (!isBlockEntity) this.ellipses.Add((Ellipse)entity);
                    break;
                case EntityType.Face3D:
                    if (!isBlockEntity) this.faces3d.Add((Face3d)entity);
                    break;
                case EntityType.Spline:
                    if (!isBlockEntity) this.splines.Add((Spline)entity);
                    break;
                case EntityType.Hatch:
                    if (!isBlockEntity) this.hatches.Add((Hatch)entity);
                    break;
                case EntityType.Insert:
                    ((Insert)entity).Block = this.blocks.Add(((Insert)entity).Block);
                    this.blocks.References[((Insert)entity).Block.Name].Add(entity);

                    foreach (Attribute attribute in ((Insert)entity).Attributes)
                    {
                        attribute.Layer = this.layers.Add(attribute.Layer);
                        this.layers.References[attribute.Layer.Name].Add(attribute);

                        attribute.LineType = this.lineTypes.Add(attribute.LineType);
                        this.lineTypes.References[attribute.LineType.Name].Add(attribute);

                        attribute.Style = this.textStyles.Add(attribute.Style);
                        this.textStyles.References[attribute.Style.Name].Add(attribute);
                    }

                    if (!isBlockEntity) this.inserts.Add((Insert)entity);
                    break;
                case EntityType.LightWeightPolyline:
                    if (!isBlockEntity) this.lwPolylines.Add((LwPolyline)entity);
                    break;
                case EntityType.Line:
                    if (!isBlockEntity) this.lines.Add((Line)entity);
                    break;
                case EntityType.Point:
                    if (!isBlockEntity) this.points.Add((Point)entity);
                    break;
                case EntityType.PolyfaceMesh:
                    if (!isBlockEntity) this.polyfaceMeshes.Add((PolyfaceMesh)entity);
                    break;
                case EntityType.Polyline:
                    if (!isBlockEntity) this.polylines.Add((Polyline)entity);
                    break;
                case EntityType.Solid:
                    if (!isBlockEntity) this.solids.Add((Solid)entity);
                    break;
                case EntityType.Text:
                    ((Text)entity).Style = this.textStyles.Add(((Text)entity).Style);
                    this.textStyles.References[((Text)entity).Style.Name].Add(entity);
                    if (!isBlockEntity) this.texts.Add((Text)entity);
                    break;
                case EntityType.MText:
                    ((MText)entity).Style = this.textStyles.Add(((MText)entity).Style);
                    this.textStyles.References[((MText)entity).Style.Name].Add(entity);
                    if (!isBlockEntity) this.mTexts.Add((MText)entity);
                    break;
                case EntityType.Image:
                    Image image = (Image)entity;
                    image.Definition = this.imageDefs.Add(image.Definition);
                    this.imageDefs.References[image.Definition.Name].Add(image);
                    ImageDefReactor reactor = new ImageDefReactor(image.Handle);
                    this.NumHandles = reactor.AsignHandle(this.NumHandles);
                    image.Definition.Reactors.Add(image.Handle, reactor);
                    if (!isBlockEntity) this.images.Add(image);
                    break;
                case EntityType.MLine:
                    ((MLine)entity).Style = this.mlineStyles.Add(((MLine)entity).Style);
                    this.mlineStyles.References[((MLine)entity).Style.Name].Add(entity);
                    if (!isBlockEntity) this.mLines.Add((MLine)entity);
                    break;
                case EntityType.Ray:
                    if (!isBlockEntity) this.rays.Add((Ray)entity);
                    break;
                case EntityType.XLine:
                    if (!isBlockEntity) this.xlines.Add((XLine)entity);
                    break;

                case EntityType.AttributeDefinition:
                    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of block definition.", "entity");

                case EntityType.Attribute:
                    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of block definition.", "entity");

                default:
                    throw new ArgumentException("The entity " + entity.Type + " is not implemented or unknown.");
            }

            return true;
        }

        internal bool RemoveEntity(EntityObject entity, bool isBlockEntity)
        {
            if (entity == null)
                return false;

            if (!this.addedEntity.ContainsKey(entity.Handle))
                return false;

            // the entities that are part of a block do not belong to any of the entities lists but to the block definition
            // and they will not be removed from the drawing database
            bool removed;

            switch (entity.Type)
            {
                case EntityType.Arc:
                    removed = this.arcs.Remove((Arc)entity);
                    break;
                case EntityType.Circle:
                    removed = this.circles.Remove((Circle)entity);
                    break;
                case EntityType.Dimension:
                    removed = this.dimensions.Remove((Dimension)entity);
                    if (removed || isBlockEntity)
                    {
                        this.dimStyles.References[((Dimension)entity).Style.Name].Remove(entity);
                        this.blocks.References[((Dimension)entity).Block.Name].Remove(entity);
                    }
                    break;
                case EntityType.Ellipse:
                    removed = this.ellipses.Remove((Ellipse)entity);
                    break;
                case EntityType.Face3D:
                    removed = this.faces3d.Remove((Face3d)entity);
                    break;
                case EntityType.Spline:
                    removed = this.splines.Remove((Spline)entity);
                    break;
                case EntityType.Hatch:
                    removed = this.hatches.Remove((Hatch)entity);
                    break;
                case EntityType.Insert:
                    removed = this.inserts.Remove((Insert)entity);
                    if (removed || isBlockEntity)
                    {
                        this.blocks.References[((Insert)entity).Block.Name].Remove(entity);
                        foreach (Attribute att in ((Insert)entity).Attributes)
                        {
                            this.layers.References[att.Layer.Name].Remove(att);
                            this.lineTypes.References[att.LineType.Name].Remove(att);
                            this.textStyles.References[att.Style.Name].Remove(att);
                        }
                    }
                    break;
                case EntityType.LightWeightPolyline:
                    removed = this.lwPolylines.Remove((LwPolyline)entity);
                    break;
                case EntityType.Line:
                    removed = this.lines.Remove((Line)entity);
                    break;
                case EntityType.Point:
                    removed = this.points.Remove((Point)entity);
                    break;
                case EntityType.PolyfaceMesh:
                    removed = this.polyfaceMeshes.Remove((PolyfaceMesh)entity);
                    break;
                case EntityType.Polyline:
                    removed = this.polylines.Remove((Polyline)entity);
                    break;
                case EntityType.Solid:
                    removed = this.solids.Remove((Solid)entity);
                    break;
                case EntityType.Text:
                    this.textStyles.References[((Text)entity).Style.Name].Remove(entity);
                    removed = this.texts.Remove((Text)entity);
                    break;
                case EntityType.MText:
                    this.textStyles.References[((MText)entity).Style.Name].Remove(entity);
                    removed = this.mTexts.Remove((MText)entity);
                    break;
                case EntityType.Image:
                    Image image = (Image)entity;
                    removed = this.images.Remove(image);
                    if (removed || isBlockEntity)
                    {
                        this.imageDefs.References[image.Definition.Name].Remove(image);
                        image.Definition.Reactors.Remove(image.Handle);
                    }
                    break;
                case EntityType.MLine:
                    removed = this.mLines.Remove((MLine)entity);
                    if (removed || isBlockEntity)
                        this.mlineStyles.References[((MLine)entity).Style.Name].Remove(entity);
                    break;
                case EntityType.Ray:
                    removed = this.rays.Remove((Ray)entity);
                    break;
                case EntityType.XLine:
                    removed = this.xlines.Remove((XLine)entity);
                    break;
                case EntityType.AttributeDefinition:
                    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

                case EntityType.Attribute:
                    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

                default:
                    throw new ArgumentException("The entity " + entity.Type + " is not implemented or unknown");
            }

            if (removed || isBlockEntity)
            {
                this.layers.References[entity.Layer.Name].Remove(entity);
                this.lineTypes.References[entity.LineType.Name].Remove(entity);
                foreach (string appReg in entity.XData.Keys)
                {
                    this.appRegistries.References[appReg].Remove(entity);
                }
                this.addedEntity.Remove(entity.Handle);
            }

            return removed;
        }

        private static DxfDocument InternalLoad(Stream stream)
        {
            DxfDocument document = new DxfDocument();
            
            DxfReader dxfReader = new DxfReader();

            dxfReader.Read(stream);

            document.addedEntity = dxfReader.AddedEntity;

            //header information
            document.comments = dxfReader.Comments;
            document.drawingVariables = dxfReader.HeaderVariables;
            document.NumHandles = Convert.ToInt32(dxfReader.HeaderVariables.HandleSeed, 16);

            //tables information
            document.appRegistries = new ApplicationRegistries(document, dxfReader.ApplicationRegistries, dxfReader.ApplicationRegistryReferences);
            document.layers = new Layers(document, dxfReader.Layers, dxfReader.LayerReferences);
            document.lineTypes = new LineTypes(document, dxfReader.LineTypes, dxfReader.LineTypeReferences);
            document.textStyles = new TextStyles(document, dxfReader.TextStyles, dxfReader.TextStyleReferences);
            document.dimStyles = new DimensionStyles(document, dxfReader.DimensionStyles, dxfReader.DimensionStyleReferences);
            document.DimensionBlocksGenerated = dxfReader.DimensionBlocksGenerated;
            document.mlineStyles = new MLineStyles(document, dxfReader.MLineStyles, dxfReader.MLineStyleReferences);
            document.ucss = new UCSs(document, dxfReader.UCSs, dxfReader.UCSReferences);
            document.blocks = new BlockRecords(document, dxfReader.Blocks, dxfReader.BlockReferences);
            document.imageDefs = new ImageDefinitions(document, dxfReader.ImageDefs, dxfReader.ImageDefReferences);
            document.groups = new Groups(document, dxfReader.Groups, dxfReader.GroupReferences);

            //entities information
            document.arcs = dxfReader.Arcs;
            document.circles = dxfReader.Circles;
            document.ellipses = dxfReader.Ellipses;
            document.points = dxfReader.Points;
            document.faces3d = dxfReader.Faces3d;
            document.solids = dxfReader.Solids;
            document.lwPolylines = dxfReader.LightWeightPolyline;
            document.polylines = dxfReader.Polylines;
            document.polyfaceMeshes = dxfReader.PolyfaceMeshes;
            document.lines = dxfReader.Lines;
            document.inserts = dxfReader.Inserts;
            document.texts = dxfReader.Texts;
            document.mTexts = dxfReader.MTexts;
            document.hatches = dxfReader.Hatches;
            document.dimensions = dxfReader.Dimensions;
            document.splines = dxfReader.Splines;
            document.images = dxfReader.Images;
            document.mLines = dxfReader.MLines;
            document.rays = dxfReader.Rays;
            document.xlines = dxfReader.XLines;

            // objects
            document.GroupNamesGenerated = dxfReader.GroupNamesGenerated;
            // we will define a new RasterVariables object in case there is none in the dxf
            if (dxfReader.RasterVariables == null)
            {
                document.rasterVariables = new RasterVariables();
                document.NumHandles = document.rasterVariables.AsignHandle(document.NumHandles);
            }
            else
                document.rasterVariables = dxfReader.RasterVariables;

            return document;
        }

        private void InternalSave(Stream stream)
        {
            if (this.drawingVariables.AcadVer < DxfVersion.AutoCad2000)
                throw new NotSupportedException("Only AutoCad2000 and newer dxf versions are supported.");

            // dictionaries
            List<DictionaryObject> dictionaries = new List<DictionaryObject>();

            DictionaryObject namedObjectDictionary = new DictionaryObject("0");
            this.NumHandles = namedObjectDictionary.AsignHandle(this.NumHandles);
            DictionaryObject baseDictionary = new DictionaryObject(namedObjectDictionary.Handle);
            this.NumHandles = baseDictionary.AsignHandle(this.NumHandles);
            namedObjectDictionary.Entries.Add(baseDictionary.Handle, "ACAD_GROUP");
            dictionaries.Add(namedObjectDictionary);
            dictionaries.Add(baseDictionary);

            // create the Group dictionary
            DictionaryObject groupDictionary = new DictionaryObject(baseDictionary.Handle);
            if (this.groups.Count > 0)
            {
                this.NumHandles = groupDictionary.AsignHandle(this.NumHandles);
                foreach (Group group in this.groups.Values)
                {
                    groupDictionary.Entries.Add(group.Handle, group.Name);
                }
                dictionaries.Add(groupDictionary);
                namedObjectDictionary.Entries.Add(groupDictionary.Handle, "ACAD_GROUP");
            }

            // create the MLine style dictionary
            DictionaryObject mLineStyleDictionary = new DictionaryObject(baseDictionary.Handle);
            if (this.mlineStyles.Count > 0)
            {
                this.NumHandles = mLineStyleDictionary.AsignHandle(this.NumHandles);
                foreach (MLineStyle mLineStyle in this.mlineStyles.Values)
                {
                    mLineStyleDictionary.Entries.Add(mLineStyle.Handle, mLineStyle.Name);
                }
                dictionaries.Add(mLineStyleDictionary);
                namedObjectDictionary.Entries.Add(mLineStyleDictionary.Handle, "ACAD_MLINESTYLE");
            }

            // create the image dictionary
            DictionaryObject imageDefDictionary = new DictionaryObject(baseDictionary.Handle);
            if (this.imageDefs.Count > 0)
            {
                this.NumHandles = imageDefDictionary.AsignHandle(this.NumHandles);
                foreach (ImageDef imageDef in this.imageDefs.Values)
                    imageDefDictionary.Entries.Add(imageDef.Handle, imageDef.Name);

                dictionaries.Add(imageDefDictionary);

                namedObjectDictionary.Entries.Add(imageDefDictionary.Handle,"ACAD_IMAGE_DICT");
                namedObjectDictionary.Entries.Add(this.rasterVariables.Handle, "ACAD_IMAGE_VARS");
            }

            this.drawingVariables.HandleSeed = Convert.ToString(this.NumHandles + ReservedHandles, 16);

            DxfWriter dxfWriter = new DxfWriter(this.drawingVariables.AcadVer);
            dxfWriter.Open(stream);
            foreach (string comment in this.comments)
            {
                dxfWriter.WriteComment(comment);
            }

            //HEADER SECTION
            dxfWriter.BeginSection(StringCode.HeaderSection);
            foreach (HeaderVariable variable in this.drawingVariables.Values)
            {
                dxfWriter.WriteSystemVariable(variable);
            }
            dxfWriter.EndSection();

            //CLASSES SECTION
            dxfWriter.BeginSection(StringCode.ClassesSection);
            dxfWriter.WriteRasterVariablesClass(1);
            if (this.imageDefs.Values.Count > 0)
            {
                dxfWriter.WriteImageDefClass(this.imageDefs.Count);
                dxfWriter.WriteImageDefRectorClass(this.images.Count);
                dxfWriter.WriteImageClass(this.images.Count);
            }
            dxfWriter.EndSection();

            //TABLES SECTION
            dxfWriter.BeginSection(StringCode.TablesSection);

            //viewport tables
            dxfWriter.BeginTable(StringCode.ViewPortTable, Convert.ToString(this.NumHandles++, 16));
            foreach (Viewport vport in this.viewports)
            {
                dxfWriter.WriteViewPort(vport);
            }
            dxfWriter.EndTable();

            //line type tables
            dxfWriter.BeginTable(StringCode.LineTypeTable, Convert.ToString(this.NumHandles++, 16));
            foreach (LineType lineType in this.lineTypes.Values)
            {
                dxfWriter.WriteLineType(lineType);
            }
            dxfWriter.EndTable();

            //layer tables
            dxfWriter.BeginTable(StringCode.LayerTable, Convert.ToString(this.NumHandles++, 16));
            foreach (Layer layer in this.layers.Values)
            {
                dxfWriter.WriteLayer(layer);
            }
            dxfWriter.EndTable();

            //text style tables
            dxfWriter.BeginTable(StringCode.TextStyleTable, Convert.ToString(this.NumHandles++, 16));
            foreach (TextStyle style in this.textStyles.Values)
            {
                dxfWriter.WriteTextStyle(style);
            }
            dxfWriter.EndTable();

            //dimension style tables
            dxfWriter.BeginTable(StringCode.DimensionStyleTable, Convert.ToString(this.NumHandles++, 16));
            foreach (DimensionStyle style in this.dimStyles.Values)
            {
                dxfWriter.WriteDimensionStyle(style);
            }
            dxfWriter.EndTable();

            //view
            dxfWriter.BeginTable(StringCode.ViewTable, Convert.ToString(this.NumHandles++, 16));
            dxfWriter.EndTable();

            //ucs
            dxfWriter.BeginTable(StringCode.UcsTable, Convert.ToString(this.NumHandles++, 16));
            foreach (UCS ucs in this.ucss.Values)
            {
                dxfWriter.WriteUCS(ucs);
            }
            dxfWriter.EndTable();

            //registered application tables
            dxfWriter.BeginTable(StringCode.ApplicationIDTable, Convert.ToString(this.NumHandles++, 16));
            foreach (ApplicationRegistry id in this.appRegistries.Values)
            {
                dxfWriter.RegisterApplication(id);
            }
            dxfWriter.EndTable();

            //block reacord table
            dxfWriter.BeginTable(StringCode.BlockRecordTable, Convert.ToString(this.NumHandles++, 16));
            foreach (Block block in this.blocks.Values)
            {
                dxfWriter.WriteBlockRecord(block.Record);
            }
            dxfWriter.EndTable();

            dxfWriter.EndSection(); //End section tables

            dxfWriter.BeginSection(StringCode.BlocksSection);
            foreach (Block block in this.blocks.Values)
            {
                dxfWriter.WriteBlock(block);
            }
            dxfWriter.EndSection(); //End section blocks

            //ENTITIES SECTION
            dxfWriter.BeginSection(StringCode.EntitiesSection);
            foreach (Ray ray in this.rays)
            {
                dxfWriter.WriteEntity(ray);
            }
            foreach (XLine xline in this.xlines)
            {
                dxfWriter.WriteEntity(xline);
            }
            foreach (Arc arc in this.arcs)
            {
                dxfWriter.WriteEntity(arc);
            }
            foreach (Circle circle in this.circles)
            {
                dxfWriter.WriteEntity(circle);
            }
            foreach (Ellipse ellipse in this.ellipses)
            {
                dxfWriter.WriteEntity(ellipse);
            }
            foreach (Point point in this.points)
            {
                dxfWriter.WriteEntity(point);
            }
            foreach (Face3d face in this.faces3d)
            {
                dxfWriter.WriteEntity(face);
            }
            foreach (Spline spline in this.splines)
            {
                dxfWriter.WriteEntity(spline);
            }
            foreach (Solid solid in this.solids)
            {
                dxfWriter.WriteEntity(solid);
            }
            foreach (Insert insert in this.inserts)
            {
                dxfWriter.WriteEntity(insert);
            }
            foreach (Line line in this.lines)
            {
                dxfWriter.WriteEntity(line);
            }
            foreach (LwPolyline pol in this.lwPolylines)
            {
                dxfWriter.WriteEntity(pol);
            }
            foreach (PolyfaceMesh pol in this.polyfaceMeshes)
            {
                dxfWriter.WriteEntity(pol);
            }
            foreach (Polyline pol in this.polylines)
            {
                dxfWriter.WriteEntity(pol);
            }
            foreach (Text text in this.texts)
            {
                dxfWriter.WriteEntity(text);
            }
            foreach (MText mText in this.mTexts)
            {
                dxfWriter.WriteEntity(mText);
            }
            foreach (Hatch hatch in this.hatches)
            {
                dxfWriter.WriteEntity(hatch);
            }
            foreach (Dimension dim in this.dimensions)
            {
                dxfWriter.WriteEntity(dim);
            }
            foreach (Image image in this.images)
            {
                dxfWriter.WriteEntity(image);
            }
            foreach (MLine mLine in this.mLines)
            {
                dxfWriter.WriteEntity(mLine);
            }
            dxfWriter.EndSection(); //End section entities

            //OBJECTS SECTION
            dxfWriter.BeginSection(StringCode.ObjectsSection);

            foreach (DictionaryObject dictionary in dictionaries)
            {
                dxfWriter.WriteDictionary(dictionary);
            }

            foreach (Group group in this.groups.Values)
            {
                dxfWriter.WriteGroup(group, groupDictionary.Handle);
            }
            foreach (MLineStyle style in this.mlineStyles.Values)
            {
                dxfWriter.WriteMLineStyle(style, mLineStyleDictionary.Handle);
            }

            dxfWriter.WriteRasterVariables(this.rasterVariables, imageDefDictionary.Handle);

            foreach (ImageDef imageDef in this.imageDefs.Values)
            {
                foreach (ImageDefReactor reactor in imageDef.Reactors.Values)
                {
                    dxfWriter.WriteImageDefReactor(reactor);
                }
                dxfWriter.WriteImageDef(imageDef, imageDefDictionary.Handle);
            }

            dxfWriter.EndSection(); //End section objects

            dxfWriter.Close();

            stream.Position = 0;
        }

        private void AddDefaultObjects()
        {
            //add default viewport
            Viewport active = Viewport.Active;
            this.NumHandles = active.AsignHandle(this.NumHandles);
            this.viewports.Add(active);

            //add default layer
            this.layers.Add(Layer.Default);

            // add default line types
            this.lineTypes.Add(LineType.ByLayer);
            this.lineTypes.Add(LineType.ByBlock);
            this.lineTypes.Add(LineType.Continuous);

            // add default blocks
            this.blocks.Add(Block.ModelSpace);
            this.blocks.Add(Block.PaperSpace);

            // add default text style
            this.textStyles.Add(TextStyle.Default);

            // add default application registry
            this.appRegistries.Add(ApplicationRegistry.Default);

            // add default dimension style
            this.dimStyles.Add(DimensionStyle.Default);

            // add default MLine style
            this.mlineStyles.Add(MLineStyle.Default);

            // raster variables
            this.rasterVariables = new RasterVariables();
            this.NumHandles = this.rasterVariables.AsignHandle(this.NumHandles);

        }

        #endregion

    }

}