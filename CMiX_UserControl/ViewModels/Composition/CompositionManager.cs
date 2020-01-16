using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class CompositionManager : ViewModel
    {
        public CompositionManager(ObservableCollection<Composition> compositions, ObservableCollection<Layer> layers, ObservableCollection<Entity> entities)
        {
            Compositions = compositions;
            LayerManager = new LayerManager(new MessageService());

            CreateCompositionCommand = new RelayCommand(p => CreateComposition(p as ICompositionContext));
            CreateSelectedCompositionCommand = new RelayCommand(p => CreateSelectedComposition(p as ICompositionContext));
            DeleteCompositionCommand = new RelayCommand(p => DeleteComposition(p as ICompositionContext));
        }

        public ObservableCollection<Composition> Compositions { get; set; }
        public LayerManager LayerManager { get; set; }
        int CompID = 0;

        public ICommand CreateCompositionCommand { get; set; }
        public ICommand CreateSelectedCompositionCommand { get; set; }
        public ICommand DeleteCompositionCommand { get; set; }
        public ICommand DuplicateCompositionCommand { get; set; }

        public Composition CreateComposition(ICompositionContext context)
        {
            System.Console.WriteLine("CompositionManager CreateComposition");
            MessageService messageService = new MessageService();
            Composition comp = new Composition(messageService, context.MessageAddress, context.Assets, context.Mementor);
            comp.Name = "Composition " + CompID.ToString();
            Compositions.Add(comp);
            CompID++;
            return comp;
        }

        public Composition CreateSelectedComposition(ICompositionContext context)
        {
            MessageService messageService = new MessageService();
            Composition comp = new Composition(messageService, context.MessageAddress, context.Assets, context.Mementor);
            comp.Name = "Composition " + CompID.ToString();
            context.SelectedComposition = comp;
            Compositions.Add(comp);
            CompID++;
            return comp;
        }

        public void DeleteComposition(ICompositionContext context)
        {
            var selectedComposition = context.SelectedComposition;
            if (selectedComposition != null)
            {
                var deletedcompo = selectedComposition as Composition;
                Compositions.Remove(deletedcompo);
                //EditableCompositions.Remove(deletedcompo);

                if (Compositions.Count > 0)
                    selectedComposition = Compositions[0];
                else if (Compositions.Count == 0)
                {
                    selectedComposition = null;
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
                newCompo.SetViewModel(compositionmodel);
                newCompo.Name = newCompo.Name + "- Copy";
                context.SelectedComposition = newCompo;
                Compositions.Add(newCompo);
            }
        }
    }
}
