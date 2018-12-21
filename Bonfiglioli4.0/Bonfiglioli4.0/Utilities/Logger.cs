using Bonfiglioli4p0;
//using Bonfiglioli4p0.DynamicComparer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utilities
{
    [Serializable]
    public class LogMessage
    {
        public enum Priority
        {
            Info,               // Usare per segnalare una semplice informazione di TRACE
            Warning,            // Usare per indicare una condizione anomala ma gestita
            Error,              // Usare per indicare un errore previsto ma non risolvibile
            Fatal,              // Usare per indicare un eccezione presa come ultima spiaggia
        }

        protected DateTime when_;       // L'istante in cui è stata generata la segnalazione
        protected Priority what_;       // Il tipo di segnalazione (Info, Warning, Error...)
        protected string text_;     // Il testo della segnalazione
        protected string file_;     // Il nome del file
        protected string method_;       // La funzione da cui proviene la segnalazione
        protected string class_;        // La classe da cui proviene la segnalazione
        protected string stack_;        // Lo stato dello stack al momento dell'eccezione




        /// <summary>
        /// Costruttore da chiamare in assenza di eccezioni
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        internal LogMessage(Priority p, String t)
        {
            when_ = DateTime.Now;
            what_ = p;
            text_ = t;
            GatherDebugInfo(p);
        }

        /// <summary>
        /// Costruttore da chiamare in presenza di eccezioni
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        internal LogMessage(Priority p, Exception e)
        {
            when_ = DateTime.Now;
            what_ = p;
            text_ = e.Message;
            GatherDebugInfo(e);
        }    
        
        /// <summary>
        /// Costruttore persoonalizzato da chiamare in presenza di eccezioni
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        internal LogMessage(Priority p, Exception e,string pCustom)
        {
            when_ = DateTime.Now;
            what_ = p;
            text_ = e.Message + " - " + pCustom;
            GatherDebugInfo(e);
        }

        /// <summary>
        /// Costruttore per caricare l'evento da file
        /// </summary>
        /// <param name="t"></param>
        /// <param name="w"></param>
        /// <param name="d"></param>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="m"></param>
        /// <param name="s"></param>
        private LogMessage(Priority t, DateTime w, string d, string f, string c, string m, string s)
        {
            when_ = w;
            what_ = t;
            text_ = d;
            file_ = f;
            method_ = m;
            class_ = c;
            stack_ = s;
        }

        /// <summary>
        /// Recupera dallo stack frame le altre informazioni da memorizzare nel log
        /// </summary>
        /// <param name="p"></param>
        private void GatherDebugInfo(Priority p)
        {
            System.Diagnostics.StackFrame frame = new System.Diagnostics.StackFrame(4);
            if (frame != null)
            {
                if (frame.GetMethod() != null)
                {
                    file_ = frame.GetFileName();
                    method_ = frame.GetMethod().Name;
                    class_ = frame.GetMethod().DeclaringType.Name;
                }
            }
        }

        /// <summary>
        /// Recupara dalla eccezione le altre informazioni da memorizzare nel log
        /// </summary>
        /// <param name="p"></param>
        private void GatherDebugInfo(Exception e)
        {
            file_ = e.Source;
            method_ = e.TargetSite.Name;
            class_ = e.TargetSite.DeclaringType.Name;
            stack_ = e.StackTrace.Trim();
        }

        /// <summary>
        /// Ritorna la data e ora della generazione del messaggio
        /// </summary>
        public DateTime When
        {
            get { return when_; }
        }

        /// <summary>
        /// Ritorna il tipo di messaggio
        /// </summary>
        public Priority Type
        {
            get { return what_; }
        }

        /// <summary>
        /// Il testo della segnalazione
        /// </summary>
        public String Description
        {
            get { return text_; }
        }

        /// <summary>
        /// Il file in cui si trova il codice che ha generato il messaggio
        /// </summary>
        public String File
        {
            get { return file_ == null ? "" : file_; }
        }

        /// <summary>
        /// Il metodo da cui è stato generato il messaggio
        /// </summary>
        public String Method
        {
            get { return method_ == null ? "" : method_; }
        }

        /// <summary>
        /// La Classe che contiene il metodo che ha generato il messaggio
        /// </summary>
        public String Class
        {
            get { return class_ == null ? "" : class_; }
        }

        /// <summary>
        /// La Stack trace fino al punto in cui è stato generato il messaggio
        /// E' presente solo se il messaggio è stato gnenerato a seguito di una eccezione
        /// </summary>
        public String Stack
        {
            get { return stack_ == null ? "" : stack_; }
        }

        /// <summary>
        /// Versione condensata di File/Class/Method
        /// </summary>
        public string Condensed
        {
            get
            {
                StringBuilder sMethod = new StringBuilder();
                sMethod.Append(File);
                if (sMethod.Length != 0) sMethod.Append(": ");
                sMethod.Append(Class);
                sMethod.Append(".");
                sMethod.Append(Method);
                return sMethod.ToString();
            }
        }

        /// <summary>
        /// Conversione a stringa dell'oggetto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder t = new StringBuilder();
            t.Append(Type).Append("|");
            t.Append(When.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("|");
            t.Append(Description).Append("|");
            t.Append(File).Append("|");
            t.Append(Class).Append("|");
            t.Append(Method).Append("|");
            t.Append(Stack);
            return t.ToString();
        }

        /// <summary>
        /// Costruisce un oggetto di tipo LogMessage da una stringa
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static internal bool Parse(string s, out LogMessage msg)
        {
            msg = null;
            bool bReturn = false;
            string[] parts = s.Split(new Char[] { '|' });
            if (parts.Length == 7)
            {
                try
                {
                    Priority t = (Priority)Enum.Parse(typeof(Priority), parts[0]);
                    DateTime d = DateTime.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                    msg = new LogMessage(t, d, parts[2], parts[3], parts[4], parts[5], parts[6]);
                    bReturn = true;
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Unrecognized log message found in log file");
                }
            }
            return bReturn;
        }
    }

    /// <summary>
    /// Coustom comparer per il dizionario
    /// I messaggi sono cosiderati uguali anche se alcuni campi differiscono
    /// </summary>
    internal class MessageComparer : IEqualityComparer<LogMessage>
    {
        public bool Equals(LogMessage x, LogMessage y)
        {
            return Join(x) == Join(y);
        }

        public int GetHashCode(LogMessage obj)
        {
            return Join(obj).GetHashCode();
        }

        private string Join(LogMessage obj)
        {
            return obj.Type + obj.Description + obj.Class + obj.File + obj.Method;
        }
    }

    /// <summary>
    /// La classe che implementa il logger delle segnalazioni
    /// E' un componente che tutte le parti della applicazione possono usare
    /// per memorizzare eventi su base temporale. Il logger mantiene uno 
    /// storico temporale , per evitare che occupare troppo spazio su disco, 
    /// un altro log cumulativo per errore.
    /// </summary>
    public static class Logger
    {
        private static LoggerImpl logger_;

        /// <summary>
        /// Il costruttore inizializza la coda dei messaggi
        /// </summary>
        static Logger()
        {
            logger_ = new LoggerImpl();
        }

        /// <summary>
        /// Aggiunge una segnalazione alla coda
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            logger_.Insert(new LogMessage(LogMessage.Priority.Info, message));
        }

        /// <summary>
        /// Aggiunge una segnalazione di warning alla coda
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            logger_.Insert(new LogMessage(LogMessage.Priority.Warning, message));
        }

        /// <summary>
        /// Aggiunge una segnalazione di errore alla coda
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            logger_.Insert(new LogMessage(LogMessage.Priority.Error, message));
        }
       
        /// <summary>
        /// Aggiunge alla coda una segnalazione di errore da ecezzione 
        /// </summary>
        /// <param name="e"></param>
        public static void Error(Exception e)
        {
            logger_.Insert(new LogMessage(LogMessage.Priority.Error, e));
            if (e.InnerException != null) logger_.Insert(new LogMessage(LogMessage.Priority.Error, e.InnerException));
        }

        /// <summary>
        /// Aggiunge una segnalazione customizzata di errore alla coda
        /// </summary>
        /// <param name="message"></param>
        public static void Error(Exception message, string pCustom)
        {
            logger_.Insert(new LogMessage(LogMessage.Priority.Error, message, pCustom));
        }

        /// <summary>
        /// Aggiunge alla coda una segnalazione di eccezione non gestita
        /// </summary>
        /// <param name="e"></param>
        public static void Fatal(Exception e)
        {
            logger_.Insert(new LogMessage(LogMessage.Priority.Fatal, e));
            if (e.InnerException != null) logger_.Insert(new LogMessage(LogMessage.Priority.Error, e.InnerException));
        }

        /// <summary>
        /// Accessor per la pagina che visualizza gli allarmi
        /// </summary>
        /// <returns></returns>
        public static EventLogger Events()
        {
            return logger_.Events();
        }

        /// <summary>
        /// Accessor per la pagina che visualizza lo storico degli allarmi
        /// </summary>
        /// <returns></returns>
        public static ArchiveLogger Archive()
        {
            return logger_.Archive();
        }

        /// <summary>
        /// Versione visualizzabile della lista dei messaggi
        /// </summary>
        public class EventLogger : BindingList<LogMessage>
        {
            [NonSerialized]
            private PropertyDescriptor sort_;
            [NonSerialized]
            private ListSortDirection direction_;

            /// <summary>
            /// Costruttore
            /// </summary>
            /// <param name="l"></param>
            public EventLogger(int size)
            {
                List<LogMessage> items = this.Items as List<LogMessage>;
                items.Capacity = size;
                this.AllowNew = false;
                this.AllowRemove = false;
            }

            /// <summary>
            /// Attiva il supporto oper il sorting
            /// </summary>
            protected override bool SupportsSortingCore
            {
                get
                {
                    return true;
                }
            }

            /// <summary>
            /// Esecuzione dell'ordinamento
            /// </summary>
            /// <param name="property"></param>
            /// <param name="direction"></param>
            protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
            {
                sort_ = property;
                direction_ = direction;
                ApplySort();
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            /// <summary>
            /// Fornisce lo stato di ordinmaento della lista
            /// </summary>
            protected override bool IsSortedCore
            {
                get
                {
                    return sort_ != null;
                }
            }

            /// <summary>
            /// Recupera la direzione dell'ordinamento
            /// </summary>
            protected override ListSortDirection SortDirectionCore
            {
                get
                {
                    return direction_;
                }
            }

            /// <summary>
            /// Recupera il descrittore dell'ordinamento
            /// </summary>
            protected override PropertyDescriptor SortPropertyCore
            {
                get
                {
                    return sort_;
                }
            }

            /// <summary>
            /// Rimuovi l'ordinamento
            /// </summary>
            protected override void RemoveSortCore()
            {
                sort_ = null;
            }

            /// <summary>
            /// Notifica dell'arrivo di un nuovo evento
            /// </summary>
            public void Update()
            {
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, -1));
                if (sort_ != null) ApplySort();
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            /// <summary>
            /// Esegue l'ordinmanto
            // </summary>
            private void ApplySort()
            {
                Bonfiglioli4p0.DynamicComparer.SortProperty[] p = { new Bonfiglioli4p0.DynamicComparer.SortProperty(sort_.Name, direction_ == ListSortDirection.Descending) };
                Bonfiglioli4p0.DynamicComparer.DynamicComparer<LogMessage> comparer = new Bonfiglioli4p0.DynamicComparer.DynamicComparer<LogMessage>(p);
                List<LogMessage> items = this.Items as List<LogMessage>;
                items.Sort(comparer);
            }

            /// <summary>
            /// Elimina un blocco di elementi
            /// </summary>
            /// <param name="index"></param>
            /// <param name="n"></param>
            internal void RemoveRange(int index, int n)
            {
                List<LogMessage> items = this.Items as List<LogMessage>;
                items.RemoveRange(index, n);
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, -1));
            }
        }


        /// <summary>
        /// Versione visualizzabile della lista dei messaggi
        /// </summary>
        [Serializable]
        public class ArchiveLogger : BindingList<ArchivedMessage>
        {
            [NonSerialized]
            private Dictionary<LogMessage, ArchivedMessage> index_;
            [NonSerialized]
            private PropertyDescriptor sort_;
            [NonSerialized]
            private ListSortDirection direction_;

            /// <summary>
            /// Costruttore
            /// </summary>
            /// <param name="l"></param>
            public ArchiveLogger()
            {
                index_ = new Dictionary<LogMessage, ArchivedMessage>(new MessageComparer());
                this.AllowEdit = false;
                this.AllowNew = false;
                this.AllowRemove = false;
            }

            /// <summary>
            /// Attiva il supporto per il sorting
            /// </summary>
            protected override bool SupportsSortingCore
            {
                get
                {
                    return true;
                }
            }

            /// <summary>
            /// Esecuzione dell'ordinamento
            /// </summary>
            /// <param name="property"></param>
            /// <param name="direction"></param>
            protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
            {
                sort_ = property;
                direction_ = direction;
                ApplySort();
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            /// <summary>
            /// Fornisce lo stato di ordinmaento della lista
            /// </summary>
            protected override bool IsSortedCore
            {
                get
                {
                    return sort_ != null;
                }
            }

            /// <summary>
            /// Recupera la direzione dell'ordinamento
            /// </summary>
            protected override ListSortDirection SortDirectionCore
            {
                get
                {
                    return direction_;
                }
            }

            /// <summary>
            /// Recupera il descrittore dell'ordinamento
            /// </summary>
            protected override PropertyDescriptor SortPropertyCore
            {
                get
                {
                    return sort_;
                }
            }

            /// <summary>
            /// Rimuovi l'ordinamento
            /// </summary>
            protected override void RemoveSortCore()
            {
                sort_ = null;
            }

            /// <summary>
            /// Notifica dell'arrivo di un nuovo evento
            /// </summary>
            public void Update()
            {
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, -1));
                if (sort_ != null) ApplySort();
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            /// <summary>
            /// Esegue l'ordinamanto
            // </summary>
            private void ApplySort()
            {
                System.Diagnostics.Trace.WriteLine(sort_.Name + " " + direction_.ToString());
                Bonfiglioli4p0.DynamicComparer.SortProperty[] p = { new Bonfiglioli4p0.DynamicComparer.SortProperty(sort_.Name, direction_ == ListSortDirection.Descending) };
                Bonfiglioli4p0.DynamicComparer.DynamicComparer<ArchivedMessage> comparer = new Bonfiglioli4p0.DynamicComparer.DynamicComparer<ArchivedMessage>(p);
                List<ArchivedMessage> items = this.Items as List<ArchivedMessage>;
                items.Sort(comparer);
            }

            /// <summary>
            /// Elimina un blocco di elementi
            /// </summary>
            /// <param name="index"></param>
            /// <param name="n"></param>
            internal void RemoveRange(int index, int n)
            {
                List<ArchivedMessage> items = this.Items as List<ArchivedMessage>;
                items.RemoveRange(index, n);
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, -1));
            }

            /// <summary>
            /// Aggiorna l'archivio inserendo un nuovo messaggio o
            /// aggiornando il contatore se già esiste
            /// </summary>
            /// <param name="msg"></param>
            internal void AddOrUpdate(LogMessage msg)
            {
                List<ArchivedMessage> items = this.Items as List<ArchivedMessage>;
                if (index_.ContainsKey(msg)) index_[msg].Add(msg);
                else
                {
                    ArchivedMessage a = new ArchivedMessage(msg);
                    items.Add(a);
                    index_.Add(msg, a);
                }
            }

            protected override void OnAddingNew(AddingNewEventArgs e)
            {
                //List<ArchivedMessage> items = this.Items as List<ArchivedMessage>;
                //if(index_.ContainsKey(msg)) index_[msg].Add(msg);
                //else
                //{
                //    ArchivedMessage a = new ArchivedMessage(msg);
                //    items.Add(a);
                //    index_.Add(msg, a);
                //}     
                base.OnAddingNew(e);
            }
        }

        /// <summary>
		/// La classe che gestisce i messaggi archiati in modo cumulativo
		/// </summary>
		[Serializable]
        public class ArchivedMessage
        {
            protected LogMessage.Priority what_;        // Il tipo di segnalazione (Info, Warning, Error...)
            protected string text_;     // Il testo della segnalazione
            protected string file_;     // Il nome del file
            protected string method_;   // La funzione da cui proviene la segnalazione
            protected string class_;        // La classe da cui proviene la segnalazione
            protected DateTime last_;       // L'istante dell'ultimo messaggio generato
            protected uint count_;      // Il contatore delle occorrenze

            /// <summary>
            /// Il costruttore di default di un archivio di messaggi
            /// </summary>
            public ArchivedMessage()
            {
                what_ = LogMessage.Priority.Info;
                last_ = DateTime.MinValue;
                count_ = 0;
            }

            /// <summary>
            /// Il costruttore di un archivio di messaggi
            /// </summary>
            public ArchivedMessage(LogMessage msg)
            {
                what_ = msg.Type;
                text_ = msg.Description;
                file_ = msg.File;
                method_ = msg.Method;
                class_ = msg.Class;
                last_ = msg.When;
                count_ = 1;
            }

            /// <summary>
            /// Aggiunge un elemento al dizionario
            /// </summary>
            /// <param name="msg"></param>
            public void Add(LogMessage msg)
            {
                last_ = msg.When;
                count_ += 1;
            }
            /// <summary>
            /// Ritorna il tipo di messaggio
            /// </summary>
            public LogMessage.Priority Type
            {
                get
                {
                    return what_;
                }
                set
                {
                    what_ = value;
                }
            }

            /// <summary>
            /// Il testo della segnalazione
            /// </summary>
            public String Description
            {
                get
                {
                    return text_;
                }
                set
                {
                    text_ = value;
                }
            }

            /// <summary>
            /// Il file in cui si trova il codice che ha generato il messaggio
            /// </summary>
            public String File
            {
                get
                {
                    return file_ == null ? "" : file_;
                }
                set
                {
                    file_ = value;
                }
            }

            /// <summary>
            /// Il metodo da cui è stato generato il messaggio
            /// </summary>
            public String Method
            {
                get
                {
                    return method_ == null ? "" : method_;
                }
                set
                {
                    method_ = value;
                }
            }

            /// <summary>
            /// La Classe che contiene il metodo che ha generato il messaggio
            /// </summary>
            public String Class
            {
                get
                {
                    return class_ == null ? "" : class_;
                }
                set
                {
                    class_ = value;
                }
            }

            /// <summary>
            /// Ritorna il conteggio
            /// </summary>
            public uint Count
            {
                get
                {
                    return count_;
                }
                set
                {
                    count_ = value;
                }
            }

            /// <summary>
            /// Ritorna la data dell'ultimo aggiornamento
            /// </summary>
            public DateTime Last
            {
                get
                {
                    return last_;
                }
                set
                {
                    last_ = value;
                }
            }
        }



        /// <summary>
        /// La classe che implementa il logger delle segnalazioni dello scada
        /// E' un componente che tutte le parti della applicazione possono usare
        /// per memorizzare eventi su base temporale. Il logger mantiene uno 
        /// storico temporale e, per evitare che occupare troppo spazio su disco, 
        /// un altro log cumulativo per errore.
        /// </summary>
        public class LoggerImpl
        {
            private object synchro_;
            private EventLogger events_;
            private ArchiveLogger archive_;
            private const int MINSIZE = 100;
            private const int MAXSIZE = 299;
            public event LogChangedHandler LogChanged;
            public delegate void LogChangedHandler(object sender, EventArgs e);

            /// <summary>
            /// Il costruttore inizializza la coda dei messaggi
            /// </summary>
            public LoggerImpl()
            {
                events_ = new EventLogger(MAXSIZE);
                archive_ = new ArchiveLogger();
                synchro_ = new object();
                Load();
            }

            /// <summary>
            /// Inserisce un messaggio al logger
            /// </summary>
            /// <param name="msg"></param>
            public void Insert(LogMessage msg)
            {
                try
                {
                    lock (synchro_)
                    {
                        events_.Add(msg);
                        AppendToLogFile(msg);
                        if (events_.Count >= MAXSIZE) Flush();
                    }
                    OnLogChanged();
                }
                catch (Exception)
                {
                }
            }

            /// <summary>
            /// Attiva l'evento di cambio log
            /// </summary>
            /// <param name="text"></param>
            protected virtual void OnLogChanged()
            {
                events_.Update();
                LogChangedHandler handler = LogChanged;
                if (handler != null) handler(this, new EventArgs());
            }

            /// <summary>
            /// Compatta e salva i dati
            /// </summary>
            private void Flush()
            {
                Compact();
                Save();
            }

            /// <summary>
            /// Determina se un messaggio è di tipo critico
            /// </summary>
            /// <param name="msg"></param>
            /// <returns></returns>
            public bool IsCritical(LogMessage msg)
            {
                return msg.Type == LogMessage.Priority.Error || msg.Type == LogMessage.Priority.Fatal;
            }

            /// <summary>
            /// Compatta il logger degli eventi trasferendo i più vecchi nello storico
            /// </summary>
            private void Compact()
            {
                if (events_.Count > MINSIZE)
                {
                    IEnumerable<LogMessage> archive = events_.Take(events_.Count - MINSIZE);
                    foreach (LogMessage msg in archive) archive_.AddOrUpdate(msg);
                    events_.RemoveRange(0, events_.Count - MINSIZE);
                }
            }

            /// <summary>
            /// Salva su disco delle informazioni presenti in memoria
            /// </summary>
            private void Save()
            {
                SaveLogFile();
                SaveArchiveFile();
            }

            /// <summary>
            /// Carica l'archivio degli eventi da disco
            /// </summary>
            private void Load()
            {
                LoadLogFile();
                LoadArchiveFile();
            }

            /// <summary>
            /// Aggiunge un evento al file degli eventi
            /// </summary>
            private void AppendToLogFile(LogMessage msg)
            {
                try
                {
                    string filename = LogFilename();
                    using (Stream f = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Read))
                    {
                        Write(msg, f);
                    }
                }
                catch (Exception)
                {
                    Logger.Warning("Error appending to log file");
                }
            }

            /// <summary>
            /// Salva su disco il file di log
            /// </summary>
            private void SaveLogFile()
            {
                try
                {
                    string filename = LogFilename();
                    using (Stream f = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        foreach (LogMessage msg in events_) Write(msg, f);
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Error writing to log file");
                }
            }

            /// <summary>
            /// Carica il file di log degli eventi
            /// </summary>
            private void LoadLogFile()
            {
                try
                {
                    string filename = LogFilename();
                    using (Stream f = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        StreamReader r = new StreamReader(f, Encoding.Unicode);
                        string text = r.ReadToEnd();
                        //string[] events = text.Split(new char[] {'§', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                        string[] events = text.Split(new char[] { '§' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in events) Read(s);
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Error reading from log file");
                }
            }

            /// <summary>
            /// Scrive un messaggio su file
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="f"></param>
            private void Write(LogMessage msg, Stream f)
            {
                UnicodeEncoding e = new UnicodeEncoding();
                string text = msg.ToString() + "§\r\n";
                f.Write(e.GetBytes(text), 0, e.GetByteCount(text));
            }

            /// <summary>
            /// Legge un evento da file
            /// </summary>
            /// <param name="text"></param>
            private void Read(string text)
            {
                LogMessage msg;
                if (LogMessage.Parse(text, out msg)) events_.Add(msg);
            }

            /// <summary>
            /// Salva l'archivio degli eventi
            /// </summary>
            private void SaveArchiveFile()
            {
                CultureInfo info = System.Threading.Thread.CurrentThread.CurrentCulture;

                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                    TextWriter writer = new StreamWriter(ArchiveFilename());
                    Type[] extratypes = { typeof(ArchivedMessage) };
                    XmlSerializer serializer = new XmlSerializer(typeof(ArchiveLogger), extratypes);
                    serializer.Serialize(writer, archive_);
                    writer.Close();
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Error writing to archive file");
                }
                finally
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = info;
                }
            }

            /// <summary>
            /// Carica l'archivio degli eventi
            /// </summary>
            private void LoadArchiveFile()
            {
                CultureInfo info = System.Threading.Thread.CurrentThread.CurrentCulture;

                try
                {
                    if (System.IO.File.Exists(ArchiveFilename()))
                    {
                        System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                        TextReader reader = new StreamReader(ArchiveFilename());
                        Type[] extratypes = { typeof(ArchivedMessage) };
                        XmlSerializer serializer = new XmlSerializer(typeof(ArchiveLogger), extratypes);
                        archive_ = (ArchiveLogger)serializer.Deserialize(reader);
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Error reading from log file");
                }
                finally
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = info;
                }
            }

            /// <summary>
            /// Genera il percorso del file degli eventi
            /// </summary>
            /// <returns></returns>
            private string LogFilename()
            {
                //return Path.Combine(Application.StartupPath, "Events.log");
                //return Path.Combine(App.Current.StartupUri.AbsolutePath, "Events.log");
                return Path.Combine(Environment.CurrentDirectory, "Events.log");
            }

            /// <summary>
            /// Genera il percorso del file degli eventi archiviati
            /// </summary>
            /// <returns></returns>
            private string ArchiveFilename()
            {
                return Path.Combine(Bonfiglioli4._0.App.Current.StartupUri.AbsolutePath, "Archived.log");
            }

            /// <summary>
            /// La lista corrente degli eventi
            /// </summary>
            /// <returns></returns>
            public EventLogger Events()
            {
                lock (synchro_)
                {
                    return events_;
                }
            }

            /// <summary>
            /// L'archivio corrente degli eventi
            /// </summary>
            /// <returns></returns>
            public ArchiveLogger Archive()
            {
                lock (synchro_)
                {
                    return archive_;
                }
            }
        }
    }

}
