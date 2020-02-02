using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Ceras;

namespace CMiX.Studio.ViewModels
{
    public class CompositionManager : ViewModel
    {
        public CompositionManager()
        {
        }

        public CerasSerializer Serializer { get; set; }
        int CompID = 0;

        public void CreateComposition(Project project)
        {
            //IComponent compo = new Composition(CompID, project.MessageAddress, project.Assets, project.Mementor);
            //project.AddComponent(compo);
            //CompID++;
        }

        public void CreateSelectedComposition(Project project)
        {
            //Composition comp = new Composition(CompID, project.MessageAddress, project.Assets, project.Mementor);
            //project.SelectedComposition = comp;
            //CompID++;
        }

        public void DeleteComposition(Project project)
        {
            //var selectedComposition = project.SelectedComposition;
            //if (selectedComposition != null)
            //{
            //    var deletedcompo = selectedComposition;
            //    project.RemoveComponent(selectedComposition);

            //    if (project.EditableComposition == selectedComposition)
            //        project.EditableComposition = null;

            //    if (project.Compositions.Count > 0)
            //        selectedComposition = project.Compositions[0];
            //    else if (project.Compositions.Count == 0)
            //    {
            //        selectedComposition = null;
            //        CompID = 0;
            //    }  
            //}
        }

        public void DuplicateComposition(Project project)
        {
            //if (project.SelectedComposition != null)
            //{
            //    CompositionModel compositionmodel = project.SelectedComposition.GetModel();

            //    Composition newCompo = new Composition(CompID, project.MessageAddress,project.Assets, project.Mementor);
            //    newCompo.SetViewModel(compositionmodel);
            //    newCompo.Name = newCompo.Name + "- Copy";
            //    project.SelectedComposition = newCompo;
            //    project.AddComponent(newCompo);
            //}
        }

        public void ImportCompo(Project project)
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();
            opendialog.Filter = "Compo (*.compmix)|*.compmix";
            opendialog.DefaultExt = "compmix";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;
                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    byte[] data = File.ReadAllBytes(folderPath);
                    CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                    CreateSelectedComposition(project);
                    project.SelectedComposition.SetViewModel(compositionmodel);
                }
            }
        }

        public void ExportCompo(Project project)
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Compo (*.compmix)|*.compmix";
            savedialog.DefaultExt = "compmix";
            savedialog.FileName = project.SelectedComposition.Name;
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CompositionModel compositionmodel = project.SelectedComposition.GetModel();
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(compositionmodel);
                File.WriteAllBytes(folderPath, data);
            }
        }
    }
}
