using System.Data;

namespace Vector.Common.BusinessLayer
{
    public static class DataValidationLayer
    {
        public static bool isDataSetNotNull(DataSet dsDataset,bool isDataExists = false)
        {
            bool isResult = false;
            if(dsDataset != null && dsDataset.Tables.Count > 0)
            {
                if(isDataExists)
                    foreach(DataTable dt in dsDataset.Tables)
                    {
                        if (dt.Rows.Count > 0)
                            isResult = true;
                    }
                else
                    isResult =  true;
            }

            return isResult;
        }
    }
}
