using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class CompositionManager
    {
        public CompositionManager(ObservableCollection<Server> servers)
        {
            Servers = servers;
        }

        public ObservableCollection<Server> Servers { get; set; }
        int CompID = 0;

        public Composition CreateComposition(ICompositionContext context)
        {
            MessageService messageService = new MessageService();
            Composition comp = new Composition(messageService, context.MessageAddress, context.Assets, context.Mementor);
            comp.Name = "Composition " + CompID.ToString();
            context.Compositions.Add(comp);
            CompID++;
            return comp;
        }

        public Composition CreateSelectedComposition(ICompositionContext context)
        {
            MessageService messageService = new MessageService();
            Composition comp = new Composition(messageService, context.MessageAddress, context.Assets, context.Mementor);
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
                {
                    context.SelectedComposition = null;
                    CompID = 0;
                }
                    
            }
        }

        public void DuplicateComposition(ICompositionContext context)
        {
            if (context.SelectedComposition != null)
            {
                CompositionModel compositionmodel = context.SelectedComposition.GetModel();

                MessageService messageService = new MessageService();
                Composition newCompo = new Composition(messageService, context.MessageAddress, context.Assets, context.Mementor);
                newCompo.PasteModel(compositionmodel);
                newCompo.Name = newCompo.Name + "- Copy";
                context.SelectedComposition = newCompo;
                context.Compositions.Add(newCompo);
            }
        }
    }
}
