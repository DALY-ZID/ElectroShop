using MiniProjet.Net.Models;

namespace MiniProjet.Net.ViewModels
{
    public class EditViewModel : CreateViewModel
    {
        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
