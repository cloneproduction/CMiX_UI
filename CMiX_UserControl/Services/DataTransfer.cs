using CMiX.ViewModels;
using CMiX.Models;
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
            layerdto.LayerName = layer.LayerName;
            layerdto.MessageAddress = layer.MessageAddress;

            CopyBeatModifier(layer.BeatModifier, layerdto.BeatModifierDTO);
            CopyContent(layer.Content, layerdto.ContentDTO);
            CopyMask(layer.Mask, layerdto.MaskDTO);
            CopyColoration(layer.Coloration, layerdto.ColorationDTO);
        }

        public void PasteLayer(LayerDTO layerdto, Layer layer)
        {
            layer.BlendMode = layerdto.BlendMode;
            layer.Fade = layerdto.Fade;
            layer.LayerName = layerdto.LayerName;
            layer.MessageAddress = layerdto.MessageAddress;

            PasteBeatModifier(layerdto.BeatModifierDTO, layer.BeatModifier);
            PasteContent(layerdto.ContentDTO, layer.Content);
            PasteMask(layerdto.MaskDTO, layer.Mask);
            PasteColoration(layerdto.ColorationDTO, layer.Coloration);
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
            contentdto.MessageAddress = content.MessageAddress;

            CopyBeatModifier(content.BeatModifier, contentdto.BeatModifierDTO);
            CopyTexture(content.Texture, contentdto.TextureDTO);
            CopyGeometry(content.Geometry, contentdto.GeometryDTO);
            CopyPostFX(content.PostFX, contentdto.PostFXDTO);
        }

        public void PasteContent(ContentDTO contentdto, Content content)
        {
            content.Enable = contentdto.Enable;
            content.MessageAddress = contentdto.MessageAddress;

            PasteBeatModifier(contentdto.BeatModifierDTO, content.BeatModifier);
            PasteTexture(contentdto.TextureDTO, content.Texture);
            PasteGeometry(contentdto.GeometryDTO, content.Geometry);
            PastePostFX(contentdto.PostFXDTO, content.PostFX);
        }


        public void CopyMask(Mask mask, MaskDTO maskdto)
        {
            maskdto.Enable = mask.Enable;
            maskdto.MessageAddress = mask.MessageAddress;

            CopyBeatModifier(mask.BeatModifier, maskdto.BeatModifierDTO);
            CopyTexture(mask.Texture, maskdto.TextureDTO);
            CopyGeometry(mask.Geometry, maskdto.GeometryDTO);
            CopyPostFX(mask.PostFX, maskdto.PostFXDTO);
        }

        public void PasteMask(MaskDTO maskdto, Mask mask)
        {
            mask.Enable = maskdto.Enable;
            mask.MessageAddress = maskdto.MessageAddress;

            PasteBeatModifier(maskdto.BeatModifierDTO, mask.BeatModifier);
            PasteTexture(maskdto.TextureDTO, mask.Texture);
            PasteGeometry(maskdto.GeometryDTO, mask.Geometry);
            PastePostFX(maskdto.PostFXDTO, mask.PostFX);
        }


        public void CopyColoration(Coloration coloration, ColorationDTO colorationdto)
        {
            colorationdto.MessageAddress = coloration.MessageAddress;

            colorationdto.ObjColor = Utils.ColorToHexString(coloration.ObjColor);
            colorationdto.BgColor = Utils.ColorToHexString(coloration.BgColor);

            CopyBeatModifier(coloration.BeatModifier, colorationdto.BeatModifierDTO);
        }

        public void PasteColoration(ColorationDTO colorationdto, Coloration coloration)
        {
            coloration.MessageAddress = colorationdto.MessageAddress;
            coloration.ObjColor = Utils.HexStringToColor(colorationdto.ObjColor);
            coloration.BgColor = Utils.HexStringToColor(colorationdto.BgColor);

            PasteBeatModifier(colorationdto.BeatModifierDTO, coloration.BeatModifier);
        }


        public void CopyGeometry(Geometry geometry, GeometryDTO geometrydto)
        {
            geometrydto.MessageAddress = geometry.MessageAddress;
            geometrydto.Count = geometry.Count;

            foreach(ListBoxFileName lbfn in geometry.GeometryPaths)
            {
                geometrydto.GeometryPaths.Add(lbfn);
            }

            geometrydto.TranslateAmount = geometry.TranslateAmount;
            geometrydto.ScaleAmount = geometry.ScaleAmount;
            geometrydto.RotationAmount = geometry.RotationAmount;
            geometrydto.Is3D = geometry.Is3D;
            geometrydto.KeepAspectRatio = geometry.KeepAspectRatio;

        }

        public void PasteGeometry(GeometryDTO geometrydto, Geometry geometry)
        {
            geometry.MessageAddress = geometrydto.MessageAddress;
            geometry.Count = geometrydto.Count;


            geometry.GeometryPaths.Clear();
            foreach (ListBoxFileName lbfn in geometrydto.GeometryPaths)
            {
                geometry.GeometryPaths.Add(lbfn);
            }

            geometry.TranslateAmount = geometrydto.TranslateAmount;
            geometry.ScaleAmount = geometrydto.ScaleAmount;
            geometry.RotationAmount = geometrydto.RotationAmount;
            geometry.Is3D = geometrydto.Is3D;
            geometry.KeepAspectRatio = geometrydto.KeepAspectRatio;
        }


        public void CopyTexture(Texture texture, TextureDTO texturedto)
        {
            texturedto.MessageAddress = texture.MessageAddress;
            
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
            texture.MessageAddress = texturedto.MessageAddress;
            
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
        }


        public void CopyPostFX(PostFX postFX, PostFXDTO postFXdto)
        {
            postFXdto.MessageAddress = postFX.MessageAddress;
            postFXdto.Feedback = postFX.Feedback;
            postFXdto.Blur = postFX.Blur;
            postFXdto.Transforms = postFX.Transforms;
            postFXdto.View = postFX.View;
        }

        public void PastePostFX(PostFXDTO postFXdto, PostFX postFX)
        {
            postFX.MessageAddress = postFXdto.MessageAddress;
            postFX.Feedback = postFXdto.Feedback;
            postFX.Blur = postFXdto.Blur;
            postFX.Transforms = postFXdto.Transforms;
            postFX.View = postFXdto.View;
        }
    }
}