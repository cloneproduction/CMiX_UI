using CMiX.MVVM;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    interface IEntityEditor : IEntityContext, ISendable, ICopyPasteModel, IUndoable
    {
    }
}
