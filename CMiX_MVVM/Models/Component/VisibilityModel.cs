using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models.Component
{
    public class VisibilityModel : Model, IModel
    {
        public VisibilityModel()
        {
            this.ID = Guid.NewGuid();
        }

        public bool IsVisible { get; set; }
    }
}
