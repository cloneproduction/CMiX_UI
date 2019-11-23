using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Commands
{
    public enum MessageCommand
    {
        VIEWMODEL_UPDATE,
        LAYER_UPDATE_BLENDMODE,
        LAYER_ADD,
        LAYER_DUPLICATE,
        LAYER_DELETE,
        LAYER_MOVE,
        OBJECT_ADD,
        OBJECT_DELETE,
        OBJECT_DUPLICATE
    }
}