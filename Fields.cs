using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Fields
    {
        public string FieldID, FieldName, ShelfNumber;
        public int FieldID_Len, FieldName_Len,ShelfNumber_Len ,Record_Len;

        public void set_FieldID(string _FieldID)
        {
            FieldID = _FieldID;
            FieldID_Len = FieldID.Length;
        }
        public void set_FieldName(string _FieldName)
        {
            FieldName = _FieldName;
            FieldName_Len = FieldName.Length;
        }
        public void set_ShelfNumber(string _ShelNumber)
        {
            ShelfNumber = _ShelNumber;
            ShelfNumber_Len = ShelfNumber.Length;
        }


        public string get_FieldID()
        {
            return FieldID;
        }
        public string get_FieldName()
        {
            return FieldName;
        }
        public string get_ShelfNumber()
        {
            return ShelfNumber;
        }

    }
}
