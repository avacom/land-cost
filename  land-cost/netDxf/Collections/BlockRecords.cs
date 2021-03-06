#region netDxf, Copyright(C) 2013 Daniel Carvajal, Licensed under LGPL.

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
using netDxf.Blocks;
using netDxf.Entities;

namespace netDxf.Collections
{
    /// <summary>
    /// Represents a collection of blocks.
    /// </summary>
    public sealed class BlockRecords :
        TableObjects<Block>
    {

        #region constructor

        internal BlockRecords(DxfDocument document)
            : base(document)
        {
        }

        internal BlockRecords(DxfDocument document, Dictionary<string, Block> list, Dictionary<string, List<DxfObject>> references)
            : base(document, list, references)
        {
        }

        #endregion

        #region override methods

        /// <summary>
        /// Adds a block to the list.
        /// </summary>
        /// <param name="block"><see cref="Block">Block</see> to add to the list.</param>
        /// <returns>
        /// If a block already exists with the same name as the instance that is being added the method returns the existing block,
        /// if not it will return the new block.
        /// </returns>
        public override Block Add(Block block)
        {
            Block add;
            if (this.list.TryGetValue(block.Name, out add))
                return add;

            this.document.NumHandles = block.AsignHandle(this.document.NumHandles);
            this.list.Add(block.Name, block);
            this.references.Add(block.Name, new List<DxfObject>());

            block.Layer = this.document.Layers.Add(block.Layer);
            this.document.Layers.References[block.Layer.Name].Add(block);

            //for new block definitions configure its entities
            foreach (EntityObject blockEntity in block.Entities)
            {
                this.document.AddEntity(blockEntity, true);
            }

            //for new block definitions configure its attributes
            foreach (AttributeDefinition attDef in block.Attributes.Values)
            {
                attDef.Layer = this.document.Layers.Add(attDef.Layer);
                this.document.Layers.References[attDef.Layer.Name].Add(attDef);

                attDef.LineType = this.document.LineTypes.Add(attDef.LineType);
                this.document.LineTypes.References[attDef.LineType.Name].Add(attDef);

                attDef.Style = this.document.TextStyles.Add(attDef.Style);
                this.document.TextStyles.References[attDef.Style.Name].Add(attDef);
            }

            return block;
        }

        /// <summary>
        /// Removes a block.
        /// </summary>
        /// <param name="name"><see cref="Block">Block</see> name to remove from the document.</param>
        /// <returns>True is the block has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved blocks or any other referenced by objects cannot be removed.</remarks>
        public override bool Remove(string name)
        {
            Block block = this[name];

            if (block == null)
                return false;

            if (block.IsReserved)
                return false;

            if (this.references[block.Name].Count != 0)
                return false;

            this.document.Layers.References[block.Layer.Name].Remove(block);

            // we will remove all entities from the block definition
            foreach (EntityObject o in block.Entities)
            {
                this.document.RemoveEntity(o, true);
            }
            // we will remove all attribute definitions from the associated layers
            foreach (AttributeDefinition attDef in block.Attributes.Values)
            {
                this.document.Layers.References[attDef.Layer.Name].Remove(attDef);
                this.document.LineTypes.References[attDef.LineType.Name].Remove(attDef);
                this.document.TextStyles.References[attDef.Style.Name].Remove(attDef);
            }

            this.references.Remove(block.Name);
            return this.list.Remove(block.Name);

        }

        /// <summary>
        /// Removes a block.
        /// </summary>
        /// <param name="block"><see cref="Block">Block</see> to remove from the document.</param>
        /// <returns>True is the block has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved blocks or any other referenced by objects cannot be removed.</remarks>
        public override bool Remove(Block block)
        {
            return Remove(block.Name);
        }

        #endregion

    }
}