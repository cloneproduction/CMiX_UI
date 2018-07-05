using CMiX.ViewModels;
using CMiX.Models;
using CMiX.Controls;

namespace CMiX.Services
{
    public class DataTransfer
    {
        public void CopyLayer(Layer layer, LayerDTO layerdto)
        {
            layerdto.BlendMode = layer.BlendMode;
            layerdto.Fade = layer.Fade;

            CopyBeatModifier(layer.BeatModifier, layerdto.BeatModifierDTO);
            CopyContent(layer.Content, layerdto.ContentDTO);
            CopyMask(layer.Mask, layerdto.MaskDTO);
            CopyColoration(layer.Coloration, layerdto.ColorationDTO);
        }

        public void PasteLayer(LayerDTO layerdto, Layer layer)
        {
            layer.MessageEnabled = false;

            layer.BlendMode = layerdto.BlendMode;
            layer.Fade = layerdto.Fade;

            PasteBeatModifier(layerdto.BeatModifierDTO, layer.BeatModifier);
            PasteContent(layerdto.ContentDTO, layer.Content);
            PasteMask(layerdto.MaskDTO, layer.Mask);
            PasteColoration(layerdto.ColorationDTO, layer.Coloration);

            layer.MessageEnabled = true;
        }


        public void CopyBeatModifier(BeatModifier beatmodifier, BeatModifierDTO beatmodifierdto)
        {
            beatmodifierdto.ChanceToHit = beatmodifier.ChanceToHit;
            beatmodifierdto.Multiplier = beatmodifier.Multiplier;
        }

        public void PasteBeatModifier(BeatModifierDTO beatmodifierdto, BeatModifier beatmodifier)
        {
            beatmodifier.MessageEnabled = false;

            beatmodifier.ChanceToHit = beatmodifierdto.ChanceToHit;
            beatmodifier.Multiplier = beatmodifierdto.Multiplier;

            beatmodifier.MessageEnabled = true;
        }


        public void CopyContent(Content content, ContentDTO contentdto)
        {
            contentdto.Enable = content.Enable;

            CopyBeatModifier(content.BeatModifier, contentdto.BeatModifierDTO);
            CopyTexture(content.Texture, contentdto.TextureDTO);
            CopyGeometry(content.Geometry, contentdto.GeometryDTO);
            CopyPostFX(content.PostFX, contentdto.PostFXDTO);
        }

        public void PasteContent(ContentDTO contentdto, Content content)
        {
            content.MessageEnabled = false;

            content.Enable = contentdto.Enable;

            PasteBeatModifier(contentdto.BeatModifierDTO, content.BeatModifier);
            PasteTexture(contentdto.TextureDTO, content.Texture);
            PasteGeometry(contentdto.GeometryDTO, content.Geometry);
            PastePostFX(contentdto.PostFXDTO, content.PostFX);

            content.MessageEnabled = true;
        }


        public void CopyMask(Mask mask, MaskDTO maskdto)
        {
            maskdto.Enable = mask.Enable;

            CopyBeatModifier(mask.BeatModifier, maskdto.BeatModifierDTO);
            CopyTexture(mask.Texture, maskdto.TextureDTO);
            CopyGeometry(mask.Geometry, maskdto.GeometryDTO);
            CopyPostFX(mask.PostFX, maskdto.PostFXDTO);
        }

        public void PasteMask(MaskDTO maskdto, Mask mask)
        {
            mask.MessageEnabled = false;

            mask.Enable = maskdto.Enable;

            PasteBeatModifier(maskdto.BeatModifierDTO, mask.BeatModifier);
            PasteTexture(maskdto.TextureDTO, mask.Texture);
            PasteGeometry(maskdto.GeometryDTO, mask.Geometry);
            PastePostFX(maskdto.PostFXDTO, mask.PostFX);

            mask.MessageEnabled = true;
        }


        public void CopyColoration(Coloration coloration, ColorationDTO colorationdto)
        {
            colorationdto.ObjColor = Utils.ColorToHexString(coloration.ObjColor);
            colorationdto.BgColor = Utils.ColorToHexString(coloration.BgColor);

            CopyBeatModifier(coloration.BeatModifier, colorationdto.BeatModifierDTO);
        }

        public void PasteColoration(ColorationDTO colorationdto, Coloration coloration)
        {
            coloration.MessageEnabled = false;

            coloration.ObjColor = Utils.HexStringToColor(colorationdto.ObjColor);
            coloration.BgColor = Utils.HexStringToColor(colorationdto.BgColor);

            PasteBeatModifier(colorationdto.BeatModifierDTO, coloration.BeatModifier);

            coloration.MessageEnabled = true;
        }


        public void CopyGeometry(Geometry geometry, GeometryDTO geometrydto)
        {
            geometrydto.Count = geometry.Count;

            foreach(ListBoxFileName lbfn in geometry.GeometryPaths)
            {
                geometrydto.GeometryPaths.Add(lbfn);
            }

            geometrydto.TranslateAmount = geometry.TranslateAmount;

            CopyGeometryTranslate(geometry.GeometryTranslate, geometrydto.GeometryTranslate);
            CopyGeometryScale(geometry.GeometryScale, geometrydto.GeometryScale);
            CopyGeometryRotation(geometry.GeometryRotation, geometrydto.GeometryRotation);

            geometrydto.ScaleAmount = geometry.ScaleAmount;
            geometrydto.RotationAmount = geometry.RotationAmount;
            geometrydto.Is3D = geometry.Is3D;
            geometrydto.KeepAspectRatio = geometry.KeepAspectRatio;
        }

        public void PasteGeometry(GeometryDTO geometrydto, Geometry geometry)
        {
            geometry.MessageEnabled = false;

            geometry.Count = geometrydto.Count;

            geometry.GeometryPaths.Clear();
            foreach (ListBoxFileName lbfn in geometrydto.GeometryPaths)
            {
                geometry.GeometryPaths.Add(lbfn);
            }

            geometry.TranslateAmount = geometrydto.TranslateAmount;
            geometry.ScaleAmount = geometrydto.ScaleAmount;
            geometry.RotationAmount = geometrydto.RotationAmount;

            PasteGeometryTranslate(geometrydto.GeometryTranslate, geometry.GeometryTranslate);
            PasteGeometryScale(geometrydto.GeometryScale, geometry.GeometryScale);
            PasteGeometryRotation(geometrydto.GeometryRotation, geometry.GeometryRotation);

            geometry.Is3D = geometrydto.Is3D;
            geometry.KeepAspectRatio = geometrydto.KeepAspectRatio;

            geometry.MessageEnabled = true;
        }


        public void CopyGeometryTranslate(GeometryTranslate geometrytranslate, GeometryTranslateDTO geometrytranslatedto)
        {
            geometrytranslatedto.TranslateModeDTO = geometrytranslate.TranslateMode;
        }

        public void PasteGeometryTranslate(GeometryTranslateDTO geometrytranslatedto, GeometryTranslate geometrytranslate)
        {
            geometrytranslate.TranslateMode = geometrytranslatedto.TranslateModeDTO;
        }


        public void CopyGeometryScale(GeometryScale geometryscale, GeometryScaleDTO geometryscaledto)
        {
            geometryscaledto.ScaleModeDTO = geometryscale.ScaleMode;
        }

        public void PasteGeometryScale(GeometryScaleDTO geometryscaledto, GeometryScale geometryscale)
        {
            geometryscale.ScaleMode = geometryscaledto.ScaleModeDTO;
        }


        public void CopyGeometryRotation(GeometryRotation geometryrotation, GeometryRotationDTO geometryrotationdto)
        {
            geometryrotationdto.RotationModeDTO = geometryrotation.RotationMode;
        }

        public void PasteGeometryRotation(GeometryRotationDTO geometryrotationdto, GeometryRotation geometryrotation)
        {
            geometryrotation.RotationMode = geometryrotationdto.RotationModeDTO;
        }


        public void CopyTexture(Texture texture, TextureDTO texturedto)
        {
            foreach (ListBoxFileName lbfn in texture.TexturePaths)
            {
                texturedto.TexturePaths.Add(lbfn);
            }

            texturedto.Brightness = texture.Brightness;
            texturedto.Contrast = texture.Contrast;
            texturedto.Saturation = texture.Saturation;
            texturedto.Keying = texture.Keying;
            texturedto.Invert = texture.Invert;
            texturedto.InvertMode = texture.InvertMode;
        }

        public void PasteTexture(TextureDTO texturedto, Texture texture)
        {
            texture.MessageEnabled = false;

            texture.TexturePaths.Clear();
            foreach (ListBoxFileName lbfn in texturedto.TexturePaths)
            {
                texture.TexturePaths.Add(lbfn);
            }

            texture.Brightness = texturedto.Brightness;
            texture.Contrast = texturedto.Contrast;
            texture.Saturation = texturedto.Saturation;
            texture.Keying = texturedto.Keying;
            texture.Invert = texturedto.Invert;
            texture.InvertMode = texturedto.InvertMode;

            texture.MessageEnabled = true;
        }


        public void CopyPostFX(PostFX postFX, PostFXDTO postFXdto)
        {
            postFXdto.Feedback = postFX.Feedback;
            postFXdto.Blur = postFX.Blur;
            postFXdto.Transforms = postFX.Transforms;
            postFXdto.View = postFX.View;
        }

        public void PastePostFX(PostFXDTO postFXdto, PostFX postFX)
        {
            postFX.MessageEnabled = false;

            postFX.Feedback = postFXdto.Feedback;
            postFX.Blur = postFXdto.Blur;
            postFX.Transforms = postFXdto.Transforms;
            postFX.View = postFXdto.View;

            postFX.MessageEnabled = true;
        }


        public void CopyRangeControl(RangeControl rangecontrol, RangeControlDTO rangecontroldto)
        {
            rangecontroldto.Range = rangecontrol.Range;
            rangecontroldto.Modifier = rangecontrol.Modifier;
        }

        public void PasteRangeControl(RangeControlDTO rangecontroldto, RangeControl rangecontrol)
        {
            rangecontrol.MessageEnabled = false;

            rangecontrol.Range = rangecontroldto.Range;
            rangecontrol.Modifier = rangecontroldto.Modifier;

            rangecontrol.MessageEnabled = true;
        }
    }
}