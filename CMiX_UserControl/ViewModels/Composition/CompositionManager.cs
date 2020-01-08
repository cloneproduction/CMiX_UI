using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class CompositionManager
    {
        public CompositionManager(MessageService messageService)
        {
            MessageService = messageService;
        }

        public MessageService MessageService { get; set; }
        int CompID = 0;

        public Composition CreateComposition(ICompositionContext context)
        {
            Composition comp = new Composition(context.MessageService, context.MessageAddress, context.Assets, context.Mementor);
            comp.Name = "Composition " + CompID.ToString();
            context.Compositions.Add(comp);
            CompID++;
            return comp;
        }

        public Composition CreateSelectedComposition(ICompositionContext context)
        {
            Composition comp = new Composition(context.Sender, context.MessageAddress, context.Assets, context.Mementor);
            comp.Name = "Composition " + CompID.ToString();
            context.SelectedComposition = comp;
            context.Compositions.Add(comp);
            CompID++;
            return comp;
        }

        public void DeleteComposition(ICompositionContext context)
        {
            var selectedComposition = context.SelectedComposition;
            if (selectedComposition != null)
            {
                var deletedcompo = selectedComposition as Composition;
                context.Compositions.Remove(deletedcompo);
                //EditableCompositions.Remove(deletedcompo);

                if (context.Compositions.Count > 0)
                    selectedComposition = context.Compositions[0];
                else if (context.Compositions.Count == 0)
                    CompID = 0;
            }
        }

        public void DuplicateComposition(ICompositionContext context)
        {
            if (context.SelectedComposition != null)
            {
                CompositionModel compositionmodel = new CompositionModel();
                context.SelectedComposition.CopyModel(compositionmodel);

                Composition newCompo = new Composition(MessageService, context.MessageAddress, context.Assets, context.Mementor);
                newCompo.PasteModel(compositionmodel);
                newCompo.Name = newCompo.Name + "- Copy";
                context.SelectedComposition = newCompo;
                context.Compositions.Add(newCompo);
            }
        }
    }
}
