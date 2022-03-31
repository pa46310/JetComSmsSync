using JetComSmsSync.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace JetComSmsSync.Modules.Protractor.Models
{
    public class AccountModel : BindableBase, ISelectable
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public string BigId { get; set; }
        public string ConnectionId { get; set; }
        public string ApiKey { get; set; }
    }
}
