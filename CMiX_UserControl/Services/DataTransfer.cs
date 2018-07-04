using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.ViewModels;
using CMiX.Models;
using System.Collections.ObjectModel;
using CMiX.Controls;

namespace CMiX.Services
{
    public class DataTransfer
    {
        public DataTransfer()
        {

        }

        public void CopyLayer(Layer layer, LayerDTO layerdto)
        {
            layerdto.BlendMode = layer.BlendMode;
            layerdto.Fade = layer.Fade;

            CopyBeatModifier(layer.BeatModifier, layerdto.BeatModifierDTO);

            #region CONTENT


            #region BEATMODIFIER
            layerdto.ContentBeatModifierChanceToHit = layer.Content.BeatModifier.ChanceToHit;
            layerdto.ContentBeatModifierMultiplier = layer.Content.BeatModifier.Multiplier;
            #endregion

            #region GEOMETRY

            layerdto.ContentGeometryCount = layer.Content.Geometry.Count;
            layerdto.ContentGeometryGeometryPaths = layer.Content.Geometry.GeometryPaths.ToList();
            layerdto.ContentGeometryIs3D = layer.Content.Geometry.Is3D;
            layerdto.ContentGeometryKeepAspectRatio = layer.Content.Geometry.KeepAspectRatio;

            layerdto.ContentGeometryTranslateAmount = layer.Content.Geometry.TranslateAmount;
            layerdto.ContentGeometryScaleAmount = layer.Content.Geometry.ScaleAmount;
            layerdto.ContentGeometryRotationAmount = layer.Content.Geometry.RotationAmount;

            #endregion

            #endregion

            #region MASK

            layerdto.MaskEnable = layer.Mask.Enable;

            #region BEATMODIFIER
            layerdto.MaskBeatModifierChanceToHit = layer.Mask.BeatModifier.ChanceToHit;
            layerdto.MaskBeatModifierMultiplier = layer.Mask.BeatModifier.Multiplier;
            #endregion

            #region GEOMETRY

            layerdto.MaskGeometryCount = layer.Mask.Geometry.Count;
            layerdto.MaskGeometryGeometryPaths = layer.Mask.Geometry.GeometryPaths.ToList();
            layerdto.MaskGeometryIs3D = layer.Mask.Geometry.Is3D;
            layerdto.MaskGeometryKeepAspectRatio = layer.Mask.Geometry.KeepAspectRatio;

            layerdto.MaskGeometryTranslateAmount = layer.Mask.Geometry.TranslateAmount;
            layerdto.MaskGeometryScaleAmount = layer.Mask.Geometry.ScaleAmount;
            layerdto.MaskGeometryRotationAmount = layer.Mask.Geometry.RotationAmount;

            #endregion

            #endregion
        }



        public void PasteLayer(LayerDTO layerdto, Layer layer)
        {
            layer.BlendMode = layerdto.BlendMode;
            layer.Fade = layerdto.Fade;

            PasteBeatModifier(layerdto.BeatModifierDTO, layer.BeatModifier);

            #region CONTENT

            #region BEATMODIFIER

            layer.Content.BeatModifier.ChanceToHit = layerdto.ContentBeatModifierChanceToHit;
            layer.Content.BeatModifier.Multiplier = layerdto.ContentBeatModifierMultiplier;

            #endregion

            #region GEOMETRY

            layer.Content.Geometry.Count = layerdto.ContentGeometryCount;
            layer.Content.Geometry.GeometryPaths.Clear();
 
            foreach(ListBoxFileName lbfn in layerdto.ContentGeometryGeometryPaths)
            {
                layer.Content.Geometry.GeometryPaths.Add(lbfn);
            }

            layer.Content.Geometry.Is3D = layerdto.ContentGeometryIs3D;
            layer.Content.Geometry.KeepAspectRatio = layerdto.ContentGeometryKeepAspectRatio;
            layer.Content.Geometry.TranslateAmount = layerdto.ContentGeometryTranslateAmount;
            layer.Content.Geometry.ScaleAmount = layerdto.ContentGeometryScaleAmount;
            layer.Content.Geometry.RotationAmount = layerdto.ContentGeometryRotationAmount;

            #endregion

            #endregion

            #region MASK

            layer.Mask.Enable = layerdto.MaskEnable;

            #region BEATMODIFIER

            layer.Mask.BeatModifier.ChanceToHit = layerdto.MaskBeatModifierChanceToHit;
            layer.Mask.BeatModifier.Multiplier = layerdto.MaskBeatModifierMultiplier;

            #endregion

            #region GEOMETRY

            layer.Mask.Geometry.Count = layerdto.MaskGeometryCount;
            layer.Mask.Geometry.GeometryPaths.Clear();

            foreach (ListBoxFileName lbfn in layerdto.MaskGeometryGeometryPaths)
            {
                layer.Mask.Geometry.GeometryPaths.Add(lbfn);
            }

            layer.Mask.Geometry.Is3D = layerdto.MaskGeometryIs3D;
            layer.Mask.Geometry.KeepAspectRatio = layerdto.MaskGeometryKeepAspectRatio;
            layer.Mask.Geometry.TranslateAmount = layerdto.MaskGeometryTranslateAmount;
            layer.Mask.Geometry.ScaleAmount = layerdto.MaskGeometryScaleAmount;
            layer.Mask.Geometry.RotationAmount = layerdto.MaskGeometryRotationAmount;

            #endregion

            #endregion
        }

        public void CopyBeatModifier(BeatModifier beatmodifier, BeatModifierDTO beatmodifierdto)
        {
            beatmodifierdto.ChanceToHit = beatmodifier.ChanceToHit;
            beatmodifierdto.Multiplier = beatmodifier.Multiplier;
        }

        public void PasteBeatModifier(BeatModifierDTO beatmodifierdto, BeatModifier beatmodifier)
        {
            beatmodifier.ChanceToHit = beatmodifierdto.ChanceToHit;
            beatmodifier.Multiplier = beatmodifierdto.Multiplier;
        }


        public void CopyContent(Content content, ContentDTO contentdto)
        {
            contentdto.Enable = content.Enable;
            CopyBeatModifier(content.BeatModifier, contentdto.BeatModifierDTO);
        }

        public void PasteContent(ContentDTO contentdto, Content content)
        {
            content.Enable = contentdto.Enable;
            PasteBeatModifier(contentdto.BeatModifierDTO, content.BeatModifier);
        }


        public void CopyMask(Content content, ContentDTO contentdto)
        {

        }

        public void PasteMask(ContentDTO contentdto, Content content)
        {

        }
    }
}