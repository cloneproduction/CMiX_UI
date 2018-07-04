using CMiX.Controls;
using CMiX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    [Serializable]
    public class LayerDTO
    {
        public LayerDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
        }

        public double Fade { get; set; }

        public string BlendMode { get; set; }

        public BeatModifierDTO BeatModifierDTO { get; set; }

        public ContentDTO ContentDTO { get; set; }

        public MaskDTO MaskDTO { get; set; }

        public ColorationDTO ColorationDTO { get; set; }



        #region CONTENT

        public double ContentBeatModifierChanceToHit { get; set; }

        public double ContentBeatModifierMultiplier { get; set; }

        public int ContentGeometryCount { get; set; }

        public List<ListBoxFileName> ContentGeometryGeometryPaths { get; set; }

        public double ContentGeometryTranslateAmount { get; set; }

        public double ContentGeometryScaleAmount { get; set; }

        public double ContentGeometryRotationAmount { get; set; }

        public bool ContentGeometryIs3D { get; set; }

        public bool ContentGeometryKeepAspectRatio { get; set; }

        #endregion

        #region MASK

        public bool MaskEnable { get; set; }

        public double MaskBeatModifierChanceToHit { get; set; }

        public double MaskBeatModifierMultiplier { get; set; }

        public int MaskGeometryCount { get; set; }

        public List<ListBoxFileName> MaskGeometryGeometryPaths { get; set; }

        public double MaskGeometryTranslateAmount { get; set; }

        public double MaskGeometryScaleAmount { get; set; }

        public double MaskGeometryRotationAmount { get; set; }

        public bool MaskGeometryIs3D { get; set; }

        public bool MaskGeometryKeepAspectRatio { get; set; }

        #endregion
    }
}
