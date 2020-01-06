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
        COMPOSITION_ADD,
        COMPOSITION_DELETE,
        COMPOSITION_DUPLICATE,
        LAYER_UPDATE_BLENDMODE,
        LAYER_ADD,
        LAYER_DUPLICATE,
        LAYER_DELETE,
        LAYER_MOVE,
        ENTITY_ADD,
        ENTITY_DELETE,
        ENTITY_DUPLICATE
    }
}