using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BLL.People;

namespace DVLDWinForms.People
{
    public partial class frmAddEditPerson : Form
    {
        #region Constructors
        private frmAddEditPerson() => InitializeComponent();

        public static frmAddEditPerson CreateNew(PersonID PersonID)
        {
            frmAddEditPerson frmAdd_EditPerson = new frmAddEditPerson();
            frmAdd_EditPerson.ucAddEditPerson1.Person.ID = PersonID;
            return frmAdd_EditPerson;
        }

        public static frmAddEditPerson CreateNew(clsPerson Person)
        {
            frmAddEditPerson frmAdd_EditPerson = new frmAddEditPerson();
            frmAdd_EditPerson.ucAddEditPerson1.Person = Person;
            return frmAdd_EditPerson;
        }
        #endregion

        #region Public Properties
        public bool IsAnyChangedHappened { get; set; }
        public clsPerson Person { get; private set; }
        #endregion


        #region Private Helper UI Methods
        private void _onNewPersonAdded(clsPerson NewPerson)
        {
            lblPersonID.Text = NewPerson.ID.ToString();
            lblTitel   .Text = "Update Person";
            this.Person = NewPerson;
            IsAnyChangedHappened = true;
        }
        
        private void _onPersonInfoSaved(clsPerson UpdatedPerson)
        {
            this.Person = UpdatedPerson;
            IsAnyChangedHappened = true;
        }
        #endregion

        #region Main UI Methods
        private void frmAdd_EditPerson_Load(object sender, EventArgs e)
        {
            if (this.ucAddEditPerson1.Person.ID.IsEmpty())
            {
                // Adding Mode
                this.ucAddEditPerson1.Person = clsPerson.Empty;
                this.ucAddEditPerson1.OnPersonInfoSaved += _onNewPersonAdded;

                this.lblPersonID.Text = "N/A";
                this.lblTitel   .Text = "Add New Person";
            }
            else
            {
                // Updating Mode
                this.ucAddEditPerson1.OnPersonInfoSaved += _onPersonInfoSaved;

                this.lblPersonID.Text = this.ucAddEditPerson1.Person.ID.ToString();
                this.lblTitel   .Text = "Update Person";
            }

            ucAddEditPerson1.ContainerForm = this;
        }
        #endregion
    }
}
