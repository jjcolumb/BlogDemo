using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Customer : BaseObject
    {
        public Customer(Session session) : base(session)
        {

        }


        string _name;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => _name;
            set => SetPropertyValue(nameof(Name), ref _name, value);
        }

        string _address;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => _address;
            set => SetPropertyValue(nameof(Address), ref _address, value);
        }

        string _code;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Code
        {
            get => _code;
            set => SetPropertyValue(nameof(Code), ref _code, value);
        }

        DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set => SetPropertyValue(nameof(Birthday), ref _birthday, value);
        }

        bool _active;
        public bool Active
        {
            get => _active;
            set => SetPropertyValue(nameof(Active), ref _active, value);
        }

        [Association("Customer-Invoices")]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoices));
            }
        }

    }
}
