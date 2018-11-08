
#region Referencias

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

#endregion

namespace DBE.Net.Facel.WebFacel.Models
{
    public class FilesDetailModel : INotifyPropertyChanged
    {

        #region Constantes privadas

        private const string TEMPLATE_REQUIRED = "El campo '{0}' es obligatorio";
        
        #endregion

        #region Miembros privados

        private int m_FileDetailId;
        private string m_companyId;
        private string m_fileName;
        private string m_fileData;
        private DateTime m_datetimeUpd;
        private string m_statusUpd;
        private string m_transactionId;
        private string m_processName;
        private string m_processStatus;
        private DateTime m_processDate;
        private string m_messageType;
        private string m_legalStatus;
        private string m_documentPrefix;
        private string m_resourceType;
        private string m_documentType;
        private string m_documentNumber;
        private string m_senderIdentificaton;
        private string m_statusDown;
        private string m_downloadData;
        private DateTime m_datetimeDown;
        private string m_faultCode;
        private string m_faultString;
        private string m_statusCode;
        private string m_reasonPhrase;
        private string m_errorMessage;

        #endregion

        #region Eventos publicos

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Atributos

        #region FileDetailId

        [Required(ErrorMessage = TEMPLATE_REQUIRED)]
        [Display(Name = "FileDetailId", Description = "")]
        public int FileDetailId
        {
            get { return this.m_FileDetailId; }
            set
            {
                if (value != this.m_FileDetailId)
                {
                    this.m_FileDetailId = value;
                    this.OnPropertyChanged("FileDetailId");
                }
            }
        }

        #endregion

        #region companyId

        [Required(ErrorMessage = TEMPLATE_REQUIRED)]
        [Display(Name = "companyId", Description = "")]
        public string companyId
        {
            get { return this.m_companyId; }
            set
            {
                if (value != this.m_companyId)
                {
                    this.m_companyId = value;
                    this.OnPropertyChanged("companyId");
                }
            }
        }

        #endregion

        #region fileName

        [Required(ErrorMessage = TEMPLATE_REQUIRED)]
        [Display(Name = "fileName", Description = "")]
        public string fileName
        {
            get { return this.m_fileName; }
            set
            {
                if (value != this.m_fileName)
                {
                    this.m_fileName = value;
                    this.OnPropertyChanged("fileName");
                }
            }
        }

        #endregion

        #region fileData

        [Required(ErrorMessage = TEMPLATE_REQUIRED)]
        [Display(Name = "fileData", Description = "")]
        public string fileData
        {
            get { return this.m_fileData; }
            set
            {
                if (value != this.m_fileData)
                {
                    this.m_fileData = value;
                    this.OnPropertyChanged("fileData");
                }
            }
        }

        #endregion

        #region datetimeUpd

        [Required(ErrorMessage = TEMPLATE_REQUIRED)]
        [Display(Name = "datetimeUpd", Description = "")]
        public DateTime datetimeUpd
        {
            get { return this.m_datetimeUpd; }
            set
            {
                if (value != this.m_datetimeUpd)
                {
                    this.m_datetimeUpd = value;
                    this.OnPropertyChanged("datetimeUpd");
                }
            }
        }

        #endregion

        #region statusUpd

        [Required(ErrorMessage = TEMPLATE_REQUIRED)]
        [Display(Name = "statusUpd", Description = "")]
        public string statusUpd
        {
            get { return this.m_statusUpd; }
            set
            {
                if (value != this.m_statusUpd)
                {
                    this.m_statusUpd = value;
                    this.OnPropertyChanged("statusUpd");
                }
            }
        }

        #endregion

        #region transactionId

        [Display(Name = "transactionId", Description = "")]
        public string transactionId
        {
            get { return this.m_transactionId; }
            set
            {
                if (value != this.m_transactionId)
                {
                    this.m_transactionId = value;
                    this.OnPropertyChanged("transactionId");
                }
            }
        }

        #endregion

        #region processName

        [Display(Name = "processName", Description = "")]
        public string processName
        {
            get { return this.m_processName; }
            set
            {
                if (value != this.m_processName)
                {
                    this.m_processName = value;
                    this.OnPropertyChanged("processName");
                }
            }
        }

        #endregion

        #region processStatus

        [Display(Name = "processStatus", Description = "")]
        public string processStatus
        {
            get { return this.m_processStatus; }
            set
            {
                if (value != this.m_processStatus)
                {
                    this.m_processStatus = value;
                    this.OnPropertyChanged("processStatus");
                }
            }
        }

        #endregion

        #region processDate

        [Display(Name = "processDate", Description = "")]
        public DateTime processDate
        {
            get { return this.m_processDate; }
            set
            {
                if (value != this.m_processDate)
                {
                    this.m_processDate = value;
                    this.OnPropertyChanged("processDate");
                }
            }
        }

        #endregion

        #region messageType

        [Display(Name = "messageType", Description = "")]
        public string messageType
        {
            get { return this.m_messageType; }
            set
            {
                if (value != this.m_messageType)
                {
                    this.m_messageType = value;
                    this.OnPropertyChanged("messageType");
                }
            }
        }

        #endregion

        #region legalStatus

        [Display(Name = "legalStatus", Description = "")]
        public string legalStatus
        {
            get { return this.m_legalStatus; }
            set
            {
                if (value != this.m_legalStatus)
                {
                    this.m_legalStatus = value;
                    this.OnPropertyChanged("legalStatus");
                }
            }
        }

        #endregion

        #region documentPrefix

        [Display(Name = "documentPrefix", Description = "")]
        public string documentPrefix
        {
            get { return this.m_documentPrefix; }
            set
            {
                if (value != this.m_documentPrefix)
                {
                    this.m_documentPrefix = value;
                    this.OnPropertyChanged("documentPrefix");
                }
            }
        }

        #endregion

        #region resourceType

        [Display(Name = "resourceType", Description = "")]
        public string resourceType
        {
            get { return this.m_resourceType; }
            set
            {
                if (value != this.m_resourceType)
                {
                    this.m_resourceType = value;
                    this.OnPropertyChanged("resourceType");
                }
            }
        }

        #endregion

        #region documentType

        [Display(Name = "documentType", Description = "")]
        public string documentType
        {
            get { return this.m_documentType; }
            set
            {
                if (value != this.m_documentType)
                {
                    this.m_documentType = value;
                    this.OnPropertyChanged("documentType");
                }
            }
        }

        #endregion

        #region documentNumber

        [Display(Name = "documentNumber", Description = "")]
        public string documentNumber
        {
            get { return this.m_documentNumber; }
            set
            {
                if (value != this.m_documentNumber)
                {
                    this.m_documentNumber = value;
                    this.OnPropertyChanged("documentNumber");
                }
            }
        }

        #endregion

        #region senderIdentificaton

        [Display(Name = "senderIdentificaton", Description = "")]
        public string senderIdentificaton
        {
            get { return this.m_senderIdentificaton; }
            set
            {
                if (value != this.m_senderIdentificaton)
                {
                    this.m_senderIdentificaton = value;
                    this.OnPropertyChanged("senderIdentificaton");
                }
            }
        }

        #endregion

        #region statusDown

        [Display(Name = "statusDown", Description = "")]
        public string statusDown
        {
            get { return this.m_statusDown; }
            set
            {
                if (value != this.m_statusDown)
                {
                    this.m_statusDown = value;
                    this.OnPropertyChanged("statusDown");
                }
            }
        }

        #endregion

        #region downloadData

        [Display(Name = "downloadData", Description = "")]
        public string downloadData
        {
            get { return this.m_downloadData; }
            set
            {
                if (value != this.m_downloadData)
                {
                    this.m_downloadData = value;
                    this.OnPropertyChanged("downloadData");
                }
            }
        }

        #endregion

        #region datetimeDown

        [Display(Name = "datetimeDown", Description = "")]
        public DateTime datetimeDown
        {
            get { return this.m_datetimeDown; }
            set
            {
                if (value != this.m_datetimeDown)
                {
                    this.m_datetimeDown = value;
                    this.OnPropertyChanged("datetimeDown");
                }
            }
        }

        #endregion

        #region faultCode

        [Display(Name = "faultCode", Description = "")]
        public string faultCode
        {
            get { return this.m_faultCode; }
            set
            {
                if (value != this.m_faultCode)
                {
                    this.m_faultCode = value;
                    this.OnPropertyChanged("faultCode");
                }
            }
        }

        #endregion

        #region faultString

        [Display(Name = "faultString", Description = "")]
        public string faultString
        {
            get { return this.m_faultString; }
            set
            {
                if (value != this.m_faultString)
                {
                    this.m_faultString = value;
                    this.OnPropertyChanged("faultString");
                }
            }
        }

        #endregion

        #region statusCode

        [Display(Name = "statusCode", Description = "")]
        public string statusCode
        {
            get { return this.m_statusCode; }
            set
            {
                if (value != this.m_statusCode)
                {
                    this.m_statusCode = value;
                    this.OnPropertyChanged("statusCode");
                }
            }
        }

        #endregion

        #region reasonPhrase

        [Display(Name = "reasonPhrase", Description = "")]
        public string reasonPhrase
        {
            get { return this.m_reasonPhrase; }
            set
            {
                if (value != this.m_reasonPhrase)
                {
                    this.m_reasonPhrase = value;
                    this.OnPropertyChanged("reasonPhrase");
                }
            }
        }

        #endregion

        #region errorMessage

        [Display(Name = "errorMessage", Description = "")]
        public string errorMessage
        {
            get { return this.m_errorMessage; }
            set
            {
                if (value != this.m_errorMessage)
                {
                    this.m_errorMessage = value;
                    this.OnPropertyChanged("errorMessage");
                }
            }
        }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Método constructor del modelo
        /// </summary>
        public FilesDetailModel() { }

        #endregion

        #region Control de Eventos

        #region OnPropertyChanged

        /// <summary>
        /// Se invoca cada vez que se actualiza el valor actual de cualquier propiedad
        /// </summary>
        /// <param name="propertyName">Nombre del atributo que cambio</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

        #region Metodos publicos estaticos

        #region Convertir

        /// <summary>
        /// Convierte DataTable a colección de objetos
        /// </summary>
        /// <param name="dtFilesDetail">Tabla con la información a convertir</param>
        /// <returns>Devuelve colección de objetos del tipo FilesDetailModel</returns>
        public static ObservableCollection<FilesDetailModel> Convertir(DataTable dtFilesDetail)
        {
            //Variable de retorno
            ObservableCollection<FilesDetailModel> coleccion = new ObservableCollection<FilesDetailModel>();

            if ((dtFilesDetail != null) && (dtFilesDetail.Rows.Count > 0))
            {
                #region Convierte DataTable a coleccion de objetos

                coleccion = new ObservableCollection<FilesDetailModel>
                    (dtFilesDetail.AsEnumerable().Select(r => new FilesDetailModel
                    {
                        FileDetailId = r.Field<int>("FileDetailId"),
                        companyId = r.Field<string>("companyId"),
                        fileName = r.Field<string>("fileName"),
                        fileData = r.Field<string>("fileData"),
                        datetimeUpd = r.Field<DateTime>("datetimeUpd"),
                        statusUpd = r.Field<string>("statusUpd"),
                        transactionId = r.Field<string>("transactionId"),
                        processName = r.Field<string>("processName"),
                        processStatus = r.Field<string>("processStatus"),
                        processDate = r.Field<DateTime>("processDate"),
                        messageType = r.Field<string>("messageType"),
                        legalStatus = r.Field<string>("legalStatus"),
                        documentPrefix = r.Field<string>("documentPrefix"),
                        resourceType = r.Field<string>("resourceType"),
                        documentType = r.Field<string>("documentType"),
                        documentNumber = r.Field<string>("documentNumber"),
                        senderIdentificaton = r.Field<string>("senderIdentificaton"),
                        statusDown = r.Field<string>("statusDown"),
                        downloadData = r.Field<string>("downloadData"),
                        datetimeDown = r.Field<DateTime>("datetimeDown"),
                        faultCode = r.Field<string>("faultCode"),
                        faultString = r.Field<string>("faultString"),
                        statusCode = r.Field<string>("statusCode"),
                        reasonPhrase = r.Field<string>("reasonPhrase"),
                        errorMessage = r.Field<string>("errorMessage")
                    }));

                #endregion

            }

            return coleccion; //Instruccion de retorno
        }

        #endregion

        #endregion

        #region Metodos privados

        #endregion

    }
}