//using qFluid.Entity;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Bonfiglioli4p0.Utilities
{
    public class myValidationClass : ValidationRule
    {
        public event EventHandler<CustomEventArgs> RaiseValidazio;
        public bool er;

        public void DoSomething(bool p_B)
        {
            OnRaiseValidazio(new CustomEventArgs(p_B));
        }

        protected virtual void OnRaiseValidazio(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = RaiseValidazio;

            if (handler != null)
            {
                e.valOK = er;

                handler(this, e);
            }
        }

        private int _min;
        private int _max;

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            Pubblicatore pub = new Pubblicatore();
            Sottoscrittore sub1 = new Sottoscrittore(pub);

            // Get and convert the value
            //string stringValue = GetBoundValue(value) as string;
            //object sdd= par

            Log.Logger.Information("Modifica anagrafica : " + (string)value);


                er = false;
            if (value != null)
            {
                if ((value) is BindingExpression)
                {
                    string hh = ((System.Windows.Data.BindingExpression)value).ResolvedSourcePropertyName;
                    //string ss = ((System.Windows.Data.BindingExpression)value);
                    if (value == null) {

                        er = false;
                        return ValidationResult.ValidResult; ;
                }
                }
                if ((value) is BindingGroup)
                {
                    er = false;
                    return ValidationResult.ValidResult; ;
                }

                string newVal = (string)value; //((System.Windows.Controls.TextBox)((System.Windows.Data.BindingExpressionBase)value).Target).Text;
                int proposedValue;

                er = true;
                if (int.TryParse(newVal, out proposedValue))
                {
                    if (proposedValue >= _min & proposedValue <= _max)
                    {
                        er = false;
                    }
                }

                if (er)
                {
                    pub.DoSomething(er);
             //       dataGridView1.Rows[e.RowIndex].ErrorText =
             //"Company Name must not be empty";

                    return new ValidationResult(false, "'" + value.ToString() + "' is not a whole number.");
                }
                pub.DoSomething(er);
                er = true;
                return new ValidationResult(true,"pio");
            }
            pub.DoSomething(er);
            return new ValidationResult(false, "'" + value.ToString() + "' value null");
        }

        private object GetBoundValue(object value)
        {
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;
                string propertyName = binding.ParentBinding.Path.Path;

                // Extract the value of the property.
                object propertyValue = dataItem.GetType().GetProperty(propertyName).GetValue(dataItem, null);

                // This is what we want.
                return propertyValue;
            }
            else
            {
                // ValidationStep was RawProposedValue or ConvertedProposedValue
                // The argument is already what we want!
                return value;
            }
        }

        private void dValidate(bool p_V)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates a DataRow value associated with the given named column
        /// </summary>
        private ValidationResult ValidateColumn(string columnName, DataRow row)
        {
            DataTable table = row.Table;
            DataColumn column = table.Columns[columnName];
            object cellValue;
            try
            {
                cellValue = row[column.ColumnName];
            }
            catch (RowNotInTableException)
            {
                return ValidationResult.ValidResult;
            }

            // check for null values
            if (cellValue == null || cellValue.Equals(string.Empty) || cellValue.Equals(System.DBNull.Value))
            {
                if (!column.AllowDBNull)
                {
                    return new ValidationResult(false, column.ColumnName + " cannot be null");
                }
                else
                {
                    return ValidationResult.ValidResult;
                }
            }

            // check string length constraints
            if (column.DataType == typeof(string))
            {
                string strVal = cellValue as string;
                if (strVal != null && strVal.Length > column.MaxLength)
                {
                    return new ValidationResult(false, "Length of " + column.ColumnName + " should not exceed " + column.MaxLength);
                }
            }

            // check for unique constraints
            if (column.Unique)
            {
                // iterate over all the rows in the table comparing the value for this column
                foreach (DataRow compareRow in row.Table.Rows)
                {
                    if (compareRow != row && cellValue.Equals(compareRow[column]))
                    {
                        return new ValidationResult(false, column.ColumnName + " must be unique");
                    }
                }
            }

            return ValidationResult.ValidResult;
        }
    }

    public class Pubblicatore
    {
        public event EventHandler<CustomEventArgs> RaiseValidazio;

        public void DoSomething(bool p_B)
        {
            OnRaiseValidazio(new CustomEventArgs(p_B));
        }

        protected virtual void OnRaiseValidazio(CustomEventArgs e)
        {
            //faccio copia e testo null per previnire il race condition
            EventHandler<CustomEventArgs> handler = RaiseValidazio;

            if (handler != null)
            {
                //e.valOK = true;

                handler(this, e);
            }
        }
    }

    public class Sottoscrittore
    {
        public Sottoscrittore(Pubblicatore pub)
        {
            pub.RaiseValidazio += HandleEventoCustom;
        }

        void HandleEventoCustom(object sender, CustomEventArgs e)
        {
            System.Diagnostics.Debug.Print(" received this message: {0}", e.valOK.ToString());
            //MessageBox.Show(id + " received this message: {0}", e.valOK.ToString());
        }
    }

    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(bool p_s)
        {
            _valOK = p_s;
        }

        private bool _valOK;

        public bool valOK
        {
            get { return _valOK; }
            set { _valOK = value; }
        }
    }
}
