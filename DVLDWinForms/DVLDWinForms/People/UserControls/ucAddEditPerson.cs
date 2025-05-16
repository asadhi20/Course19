using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.ComponentModel;
using DVLDWinForms.Properties;
using System.Threading.Tasks;
using System.Collections.Generic;
using Helper.Extensions;
using DVLD_BLL;
using DVLD_BLL.People;

namespace DVLDWinForms.People.UserControls
{
    public sealed partial class ucAddEditPerson : UserControl
    {
        #region Constructors
        public ucAddEditPerson() => InitializeComponent();
        #endregion


        #region Private Static Fields
        private static IEnumerable<clsCountry> _countries;
        #endregion

        #region Private Fields
        bool _isAddPersonMode { get; set; }
        string _fileOfPeopleImages = "C:\\DVLD-People-Images\\";
        string _previousImagePath { get; set; }
        #endregion
        

        #region Public Properties
        public Form ContainerForm { get; set; }
        public Action<clsPerson> OnPersonInfoSaved;
        public clsPerson Person = clsPerson.Empty;
        #endregion


        #region Private Helper Methods
        private bool _deleteImageFromFile(string ImagePath)
        {
            if (File.Exists(ImagePath)) 
                try { 
                    File.Delete(ImagePath);
                    return !File.Exists(ImagePath);
                } catch { }

            return false;
        }

        private bool _isPersonAlreadyExists(clsPerson Person)
        {
            bool a = clsPerson.IsExistsByFullName(Person.FirstName, Person.SecondName, Person.ThirdName, Person.LastName)
                  || clsPerson.IsExistsByPhone(Person.Phone)
                  || clsPerson.IsExistsByAddress(Person.Address);

            bool s = Person.Email.NotIsNullOrEmptyOrWhiteSpace() ? clsPerson.IsExistsByEmail(Person.Email) : false;
            bool d = Person.ImagePath.NotIsNullOrEmptyOrWhiteSpace() ? clsPerson.IsExistsByImagePath(Person.ImagePath) : false;

            return a || s || d;
        }
        #endregion 


        #region Private Helper Methods For UI
        private string _getFileType(OpenFileDialog openFileDialog)
            => openFileDialog.FileName.IsNullOrEmptyOrWhiteSpace() ? string.Empty
             : string.Concat(openFileDialog.FileName.SkipWhile(c => c != '.'));

        private string _generateNewImagePath(string filePathOfPeopleImages, string imageFileType)
            => imageFileType.IsNullOrEmptyOrWhiteSpace() ? string.Empty
             : filePathOfPeopleImages + Guid.NewGuid().ToString() + imageFileType;

        private bool _copyImageToFile(string originalImagePath, string newImageFilePath)
        {
            if (originalImagePath.IsNullOrEmptyOrWhiteSpace() || newImageFilePath.IsNullOrEmptyOrWhiteSpace()) return false;
            File.Copy(originalImagePath, newImageFilePath);
            return File.Exists(newImageFilePath);
        }

        private (bool isImageCopied, string newImageFilePath)_copyImageToNewFile(OpenFileDialog openFileDialog, string filePathOfPeopleImages)
        {
            string newImageFilePath = _generateNewImagePath(filePathOfPeopleImages: filePathOfPeopleImages, imageFileType: _getFileType(openFileDialog: openFileDialog));
            return (isImageCopied: _copyImageToFile(originalImagePath: openFileDialog.FileName, newImageFilePath: newImageFilePath), newImageFilePath);
        }


        private clsPerson _fullPersonFromControlsWithOutImagePath(clsPerson person)
        {
            person. NationalNo = this.mtxtNationalNo.Text.RemoveWhiteSpaces().ToUpper();
            person.  FirstName = this.txtFirstName  .Text.Trim();
            person. SecondName = this.txtSecondName .Text.Trim();
            person.  ThirdName = this.txtThirdName  .Text.Trim();
            person.   LastName = this.txtLastName   .Text.Trim();
            person.DateOfBirth = this.dtpDataOfBirth.Value;
            person.     Gender = this.rbFemale      .Checked;
            person.    Address = this.txtAddress    .Text.Trim();
            person.      Phone = this.mtxtPhone     .Text.Replace("-", string.Empty).RemoveWhiteSpaces();
            person.      Email = this.txtEmail      .Text.Trim();
            person.NationalityCountryID = CountryID.CreateNew(this.cmbCountry.SelectedIndex + 1);

            return person;
        }
        
        private clsPerson _createPersonFromUserInput()
        {
            clsPerson newPerson = _fullPersonFromControlsWithOutImagePath(clsPerson.Empty);
            
            newPerson.ImagePath = pbPersonImage.InitialImage != Resources.person_man
                               && pbPersonImage.InitialImage != Resources.person_woman
                                ? pbPersonImage.ImageLocation
                                : string.Empty;

            return newPerson;
        }


        private bool _savePersonInfoInAddingMode()
        {
            Person = _createPersonFromUserInput();

            if (_isPersonAlreadyExists(Person))
            {
                MessageBox.Show("This person is already exsits.", "An Error Occorded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string newImagePath = ofdSetImagePath.FileName.IsNullOrEmptyOrWhiteSpace()
                                ? string.Empty : ofdSetImagePath.FileName;

            bool isImageCopied = false;

            if (newImagePath.NotIsNullOrEmptyOrWhiteSpace()) (isImageCopied, Person.ImagePath) = _copyImageToNewFile(openFileDialog: ofdSetImagePath, filePathOfPeopleImages: _fileOfPeopleImages);
            else isImageCopied = true;

            if (isImageCopied && Person.Save())
            {
                if (Person.ImagePath.NotIsNullOrEmptyOrWhiteSpace()) pbPersonImage.LoadAsync(Person.ImagePath);
                
                MessageBox.Show($"Person with ID = {Person.ID.Value} Is Added.", "Adding Person Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnPersonInfoSaved?.Invoke(Person);
                _isAddPersonMode = false;
                return true;
            }

            MessageBox.Show("Person Is Not Added.", "Adding Person Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private bool _isImageInPeopleImagesFile(string peopleImagesFilePath, string imagePath) => 
            File.Exists(imagePath) && peopleImagesFilePath == imagePath.Substring(0, imagePath.LastIndexOf('\\') + 1);

        private bool _editPerson()
        {
            _fullPersonFromControlsWithOutImagePath(Person);

            Person.ImagePath = pbPersonImage.ImageLocation != Person.ImagePath && File.Exists(pbPersonImage.ImageLocation)
                             ? pbPersonImage.ImageLocation
                             : ofdSetImagePath.FileName != Person.ImagePath && File.Exists(ofdSetImagePath.FileName)
                             ? ofdSetImagePath.FileName
                             : Person.ImagePath;

            bool previousImagePath_IsNullOrEmptyOrWhiteSpace = _previousImagePath.IsNullOrEmptyOrWhiteSpace();
            bool person_ImagePath_IsNullOrEmptyOrWhiteSpace = Person.ImagePath.IsNullOrEmptyOrWhiteSpace();
            bool ofdSetImagePath_FileName_IsNullOrEmptyOrWhiteSpace = ofdSetImagePath.FileName.IsNullOrEmptyOrWhiteSpace();

            if (Person.ImagePath == _previousImagePath || previousImagePath_IsNullOrEmptyOrWhiteSpace && person_ImagePath_IsNullOrEmptyOrWhiteSpace
                && !ofdSetImagePath_FileName_IsNullOrEmptyOrWhiteSpace) return Person.Save();

            
            bool isOperationOnPersonImageDone = false;

            if (_isImageInPeopleImagesFile(peopleImagesFilePath: _fileOfPeopleImages, imagePath: _previousImagePath))
            {
                if (pbPersonImage.ImageLocation != _previousImagePath) {
                    isOperationOnPersonImageDone = _deleteImageFromFile(_previousImagePath);
                    _previousImagePath = string.Empty;
                }

                if (isOperationOnPersonImageDone && !person_ImagePath_IsNullOrEmptyOrWhiteSpace) 
                    (isOperationOnPersonImageDone, Person.ImagePath) = _copyImageToNewFile(openFileDialog: ofdSetImagePath, filePathOfPeopleImages: _fileOfPeopleImages);
            }
            else
            {
                if (!previousImagePath_IsNullOrEmptyOrWhiteSpace && person_ImagePath_IsNullOrEmptyOrWhiteSpace 
                    && ofdSetImagePath_FileName_IsNullOrEmptyOrWhiteSpace && File.Exists(_previousImagePath))
                {
                    isOperationOnPersonImageDone = _deleteImageFromFile(_previousImagePath);
                    _previousImagePath = string.Empty;
                }
                else (isOperationOnPersonImageDone, Person.ImagePath) = _copyImageToNewFile(openFileDialog: ofdSetImagePath, filePathOfPeopleImages: _fileOfPeopleImages);
            }

            return isOperationOnPersonImageDone && Person.Save();
        }

        private bool _savePersonInfoInEdittingMode()
        {
            if (!clsPerson.IsExists(Person.ID))
            {
                MessageBox.Show($"Person with ID = {Person.ID.Value} is not exsits.", "An Error Occurded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return _editPerson();
        }

        private void _loadControlWithPersonInfo(clsPerson Person)
        {
            mtxtNationalNo. Text = Person.NationalNo;
            txtFirstName.   Text = Person.FirstName;
            txtSecondName.  Text = Person.SecondName;
            txtLastName.    Text = Person.LastName;
            txtAddress.     Text = Person.Address;
            mtxtPhone.      Text = Person.Phone;
            dtpDataOfBirth.Value = Person.DateOfBirth;
            cmbCountry.SelectedItem = _countries.First(counry => counry.ID == Person.NationalityCountryID).CountryName;

            if (Person.Gender) rbFemale.Checked = true;
            else               rbMale  .Checked = true;

            txtThirdName.Text   = Person.ThirdName.IsNullOrEmptyOrWhiteSpace() ? string.Empty : Person.ThirdName;
            txtEmail.Text       = Person.Email    .IsNullOrEmptyOrWhiteSpace() ? string.Empty : Person.Email;


            _loadImagePerson(Person: Person);
        }

        private void _loadImagePerson(clsPerson Person)
        {
            if (Person.ImagePath.IsNullOrEmptyOrWhiteSpace() || !File.Exists(Person.ImagePath))
            {
                pbPersonImage.Image = Person.Gender ? Resources.person_woman : Resources.person_man;
                pbPersonImage.ImageLocation = null;
                linklblRemove.Visible = false;
            }
            else
            {
                string previosImagePath = null;
                if (pbPersonImage.ImageLocation != Person.ImagePath && File.Exists(pbPersonImage.ImageLocation))
                    previosImagePath = pbPersonImage.ImageLocation;

                pbPersonImage.LoadAsync(Person.ImagePath);

                if (!(previosImagePath is null))
                    try { if (File.Exists(previosImagePath)) File.Delete(previosImagePath); }
                    catch { }

                linklblRemove.Visible = true;
            }
        }

        private void _loadControlAsEditPersonMode()
        {
            _loadControlWithPersonInfo(Person: Person);

            _previousImagePath = Person.ImagePath;
        }


        private void _fullComboBoxCountryAsync() => 
            cmbCountry.Items.AddRange(_countries.
                Select(country => country.CountryName).
                ToArray());


        private void _showMessageUsingErrorProviderWillValidatingATextBoxBase(CancelEventArgs e,
            ErrorProvider errorProvider, TextBoxBase textBoxBase, string message)
        {
            if (textBoxBase.Text.IsNullOrEmptyOrWhiteSpace())
            {
                e.Cancel = true;
                textBoxBase.Focus();
                errorProvider.SetError(textBoxBase, message);
            }
            else {
                e.Cancel = false;
                errorProvider.SetError(textBoxBase, string.Empty);
            }
        }
        #endregion


        #region Main UI Methods
        private async void ucPerson_Load(object sender, EventArgs e)
        {
            if (_countries is null) _countries = await clsLazySingleton.Instance.GetCountriesAsync();
            _fullComboBoxCountryAsync();

            dtpDataOfBirth.MaxDate = DateTime.Today.AddYears(-18);

            if (this.Person.ID.IsEmpty())
            {
                // Adding Mode
                _isAddPersonMode = true;
                dtpDataOfBirth.Value = dtpDataOfBirth.MaxDate;

                cmbCountry.SelectedItem = "Iraq";
                return;
            }

            // Editting Mode
            if (clsPerson.IsEmpty(this.Person)) this.Person = await clsPerson.FindAsync(Person.ID);

            _loadControlAsEditPersonMode();
            linklblRemove.Visible = Person.ImagePath.NotIsNullOrEmptyOrWhiteSpace();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Do you want to save person info ?", "Save Person Info",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            if (_isAddPersonMode)
            {
                if (_savePersonInfoInAddingMode()) ucPerson_Load(this, EventArgs.Empty);
                return;
            }

            if (!_savePersonInfoInEdittingMode()) return;

            OnPersonInfoSaved?.Invoke(this.Person);
            MessageBox.Show("Data saved successfully.", "Data Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e) => ContainerForm?.Close();

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation.IsNullOrEmptyOrWhiteSpace()) 
                pbPersonImage.Image = rbMale.Checked ? Resources.person_man : Resources.person_woman;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation.IsNullOrEmptyOrWhiteSpace()) 
                pbPersonImage.Image = rbFemale.Checked ? Resources.person_woman : Resources.person_man;
        }


        private void linklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdSetImagePath.InitialDirectory = "C:\\Users\\Mr Abbas Ahmed\\Downloads";
            ofdSetImagePath.Filter = "Files (*.png) | *.png| Files (*.jpg)|*.jpg";
            ofdSetImagePath.FilterIndex = 2;

            if (ofdSetImagePath.ShowDialog() == DialogResult.OK)
            {
                _previousImagePath = Person.ImagePath;
                Person.ImagePath = ofdSetImagePath.FileName;
                pbPersonImage.LoadAsync(Person.ImagePath);
                linklblRemove.Visible = true;
            }
        }

        private void linklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.Image = rbMale.Checked ? Resources.person_man : Resources.person_woman;
            pbPersonImage.ImageLocation = null;
            pbPersonImage.Refresh();

            Person.ImagePath      = ofdSetImagePath.FileName = string.Empty;
            linklblRemove.Visible = false;
        }


        private void txtFirstName_Validating(object sender, CancelEventArgs e) => 
            _showMessageUsingErrorProviderWillValidatingATextBoxBase(e, erprovIsValidInput, txtFirstName, "First name field shuold have a value!");

        private void txtSecondName_Validating(object sender, CancelEventArgs e) => 
            _showMessageUsingErrorProviderWillValidatingATextBoxBase(e, erprovIsValidInput, txtSecondName, "Second name field should have a value!");

        private void txtLastName_Validating(object sender, CancelEventArgs e) => 
            _showMessageUsingErrorProviderWillValidatingATextBoxBase(e, erprovIsValidInput, txtLastName, "Last name field should have a value!");

        private void mtxtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.Concat(mtxtNationalNo.Text.Skip(1)).IsNullOrEmptyOrWhiteSpace())
            {
                e.Cancel = true;
                mtxtNationalNo.Focus();
                erprovIsValidInput.SetError(mtxtNationalNo, "National Number field should have a value!");
            }
            else if (erprovIsValidInput.GetError(mtxtNationalNo).NotIsNullOrEmptyOrWhiteSpace())
            {
                mtxtNationalNo_TextChanged(sender: sender, e: e);
            }
            else { 
                e.Cancel = false;
                erprovIsValidInput.SetError(mtxtNationalNo, string.Empty); 
            }
        }

        private void mtxtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.Concat(mtxtPhone.Text.Where(c => char.IsLetterOrDigit(c))).IsNullOrEmptyOrWhiteSpace())
            {
                e.Cancel = true;
                mtxtPhone.Focus();
                erprovIsValidInput.SetError(mtxtPhone, "Phone field should have a value!");
            }
            else if (erprovIsValidInput.GetError(mtxtPhone).NotIsNullOrEmptyOrWhiteSpace()) {
                mtxtPhone_TextChanged(sender: sender, e: e);
            }
            else {
                e.Cancel = false;
                erprovIsValidInput.SetError(mtxtPhone, string.Empty);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string email = txtEmail.Text.RemoveWhiteSpaces();

            if (email.NotIsNullOrEmptyOrWhiteSpace()) {
                string message = string.Empty;
                bool isFocusNeeded;

                if (isFocusNeeded = char.IsDigit(email[0])) message = "Email field should not start with number!";
                else if (isFocusNeeded = !email.Contains('@')) message = "Email field should has \'@\'!";
                else if (isFocusNeeded = email.SkipWhile(c => c != '@').Skip(1).TakeWhile(c => c != '.').Count() == 0) message = "Email field should has an domain after \'@\' such as \'gmail\'!";
                else if (isFocusNeeded = !email.Contains('.')) message = "Email field should has \'.\'!";
                else if (isFocusNeeded = email.SkipWhile(c => c != '.').Count() == 1) message = "Email field should has an domain after \'.\' such as \'com\'!";

                else if (isFocusNeeded = _isAddPersonMode ? clsPerson.IsExistsByEmail(email) : Person.Email != email ? clsPerson.IsExistsByEmail(email) : false)
                    message = "Email is used form another person!";

                erprovIsValidInput.SetError(txtEmail, message);
                if (isFocusNeeded) e.Cancel = true;
            }
            else {
                e.Cancel = false;
                erprovIsValidInput.SetError(txtEmail, string.Empty);
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e) => 
            _showMessageUsingErrorProviderWillValidatingATextBoxBase(e, erprovIsValidInput, txtAddress, "Address field should have a value!");

        
        private void mtxtNationalNo_TextChanged(object sender, EventArgs e)
        {
            string nationalNo = mtxtNationalNo.Text.RemoveWhiteSpaces().ToUpper();
            string message = string.Empty;
            bool isFocusNeeded;
            const int minimumNationalNoLength = 2;

            if (isFocusNeeded = nationalNo.Length < minimumNationalNoLength) message = "National No field should start with \'N\' and remained value must be positive number!";

            else if (isFocusNeeded = _isAddPersonMode ? clsPerson.IsExistsByNationalNo(nationalNo) : Person.NationalNo != nationalNo ? clsPerson.IsExistsByNationalNo(nationalNo) : false)
                message = "National Number is used form another person!";

            erprovIsValidInput.SetError(mtxtNationalNo, message);
            if (isFocusNeeded) mtxtNationalNo.Focus();
        }
        
        private void mtxtPhone_TextChanged(object sender, EventArgs e)
        {
            string phone = mtxtPhone.Text.Replace("-", string.Empty).RemoveWhiteSpaces();
            string message = string.Empty;
            bool isFocusNeeded;
            const int minimumNumberOfPhoneDigits = 6;

            if (isFocusNeeded = phone.Length < minimumNumberOfPhoneDigits) message = $"Phone field value must be {minimumNumberOfPhoneDigits} numbers without spaces!";

            else if (isFocusNeeded = _isAddPersonMode ? clsPerson.IsExistsByPhone(phone) : Person.Phone != phone ? clsPerson.IsExistsByPhone(phone) : false)
                message = "Phone is used form another person!";

            erprovIsValidInput.SetError(mtxtPhone, message);
            if (isFocusNeeded) mtxtPhone.Focus();
        }
        #endregion
    }
}
