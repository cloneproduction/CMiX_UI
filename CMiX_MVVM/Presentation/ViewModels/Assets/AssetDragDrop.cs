// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;

namespace CMiX.Core.Presentation.ViewModels.Assets
{
    public class AssetDragDrop
    {
        public AssetDragDrop()
        {
            SourceCollection = new ObservableCollection<Asset>();
        }

        public Asset DragObject { get; set; }
        public ObservableCollection<Asset> SourceCollection { get; set; }
    }
}