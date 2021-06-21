using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models.Component
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
