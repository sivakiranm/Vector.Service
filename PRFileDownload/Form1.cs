using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vector.Common.DataLayer;
using Vector.Master.BusinessLayer;

namespace PRFileDownload
{
    public partial class Form1 : Form
    {

        string connectionString = "Server=tcp:vectorsql.database.windows.net,1433;Initial Catalog=Vectorproddb-2022-3-9-4-23;Persist Security Info=False;User ID=Srinivas;Password=Counter@12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {




                using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
                {

                    String dictonaryPath = @"C:\Srinivas\Vector\Documents\PRDocuments\BatchDetails\";


                    DataSet DSData = objMasterDataBL.GetDownloadInfo("ClientInvoices", txtSearchText.Text);

                    //foreach (DataRow dr in DSData.Tables[1].Rows)
                    //{

                    //        DirectoryInfo di = new DirectoryInfo(dictonaryPath + Convert.ToString(dr["BatchNo"]));
                    //        // Get a reference to each file in that directory.
                    //        FileInfo[] fiArr = di.GetFiles();

                    //        foreach (var file in fiArr)
                    //            if (file.Length == 0)
                    //            {
                    //                File.Delete(dictonaryPath + Convert.ToString(dr["BatchNo"]) + "/" + file.Name);
                    //            }

                    //}




                    foreach (DataRow dr in DSData.Tables[0].Rows)
                    {
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            using (System.Net.WebClient client = new System.Net.WebClient())
                            {

                                string fileDictionaryPath = Convert.ToString(dr["BatchNo"]) + "/" + Convert.ToString(dr["ImageFileName"]);

                                if (!Directory.Exists(dictonaryPath + Convert.ToString(dr["BatchNo"])))
                                {
                                    Directory.CreateDirectory(dictonaryPath + Convert.ToString(dr["BatchNo"]));
                                }



                                if (!File.Exists(dictonaryPath + fileDictionaryPath))
                                    try
                                    {
                                        client.DownloadFile(new Uri(Convert.ToString(dr["WebUrl"])), dictonaryPath + fileDictionaryPath);
                                    }
                                    catch
                                    {

                                    }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {



            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
            {

                String dictonaryPath = @"C:\Srinivas\Vector\Documents\PRDocuments\Baseline\";


                DataSet DSData = objMasterDataBL.GetDownloadInfo("BaselineDocuments", txtSearchText.Text);




                foreach (DataRow dr in DSData.Tables[0].Rows)
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {

                            string fileDictionaryPath = Convert.ToString(dr["PRBaselineNo"]) + "/" + Convert.ToString(dr["DocumentName"]);

                            if (!Directory.Exists(dictonaryPath + Convert.ToString(dr["PRBaselineNo"])))
                            {
                                Directory.CreateDirectory(dictonaryPath + Convert.ToString(dr["PRBaselineNo"]));
                            }



                            if (!File.Exists(dictonaryPath + fileDictionaryPath))
                                try
                                {
                                    client.DownloadFile(new Uri(Convert.ToString(dr["WebUrl"])), dictonaryPath + fileDictionaryPath);
                                }
                                catch
                                {

                                }
                        }
                    }
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
            {

                String dictonaryPath = @"C:\Srinivas\Vector\Documents\PRDocuments\BatchDetails\";


                DataSet DSData = objMasterDataBL.GetDownloadInfo("Invoices", txtSearchText.Text);




                foreach (DataRow dr in DSData.Tables[0].Rows)
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {

                            string fileDictionaryPath = Convert.ToString(dr["BatchNo"]) + "/Invoice/" + Convert.ToString(dr["VectorInvoiceNo"]) + "/";
                            string summaryFileName = Convert.ToString(dr["VectorInvoiceNo"]) + "_summary.pdf";
                            string vectorFileName = Convert.ToString(dr["VectorInvoiceNo"]) + "_Vector.pdf";
                            string  vendorFileName = Convert.ToString(dr["VectorInvoiceNo"]) + "_Vendor.pdf";

                            if (!Directory.Exists(dictonaryPath + fileDictionaryPath))
                            {
                                Directory.CreateDirectory(dictonaryPath + fileDictionaryPath);
                            }



                            if (!File.Exists(dictonaryPath + fileDictionaryPath + summaryFileName))
                                try
                                {
                                    client.DownloadFile(new Uri(Convert.ToString(dr["Summary"])), dictonaryPath + fileDictionaryPath + summaryFileName);
                                }
                                catch
                                {

                                }


                            if (!File.Exists(dictonaryPath + fileDictionaryPath + vectorFileName))
                                try
                                {
                                    client.DownloadFile(new Uri(Convert.ToString(dr["Vector"])), dictonaryPath + fileDictionaryPath + vectorFileName);
                                }
                                catch
                                {

                                }



                            if (!File.Exists(dictonaryPath + fileDictionaryPath + vendorFileName))
                                try
                                {
                                    client.DownloadFile(new Uri(Convert.ToString(dr["Vendor"])), dictonaryPath + fileDictionaryPath + vendorFileName);
                                }
                                catch
                                {

                                }

                        }
                    }
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
            {

                String dictonaryPath = @"C:\Srinivas\Vector\Documents\PRDocuments\PRMIssing\";


                DataSet DSData = objMasterDataBL.GetDownloadInfo("InvoicesForProcessing", txtSearchText.Text);




                foreach (DataRow dr in DSData.Tables[0].Rows)
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {

                            string fileDictionaryPath = Convert.ToString(dr["BatchNo"]) + "/";
                            string fileName = Convert.ToString(dr["ImageFileName"]);

                            if (!Directory.Exists(dictonaryPath + fileDictionaryPath))
                            {
                                Directory.CreateDirectory(dictonaryPath + fileDictionaryPath);
                            }




                            if (!File.Exists(dictonaryPath + fileDictionaryPath + fileName))
                                try
                                {
                                    client.DownloadFile(new Uri(Convert.ToString(dr["ImagePath"])), dictonaryPath + fileDictionaryPath + fileName);
                                }
                                catch
                                {

                                } 
                        }
                    }
                }


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
            {

                String dictonaryPath = @"C:\Srinivas\Vector\Documents\PRDocuments\Contracts\";


                DataSet DSData = objMasterDataBL.GetDownloadInfo("ContractDocuments", txtSearchText.Text);




                foreach (DataRow dr in DSData.Tables[0].Rows)
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {

                            string fileDictionaryPath = Convert.ToString(dr["ContractNo"]) + "/" + Convert.ToString(dr["DocumentName"]);

                            if (!Directory.Exists(dictonaryPath + Convert.ToString(dr["ContractNo"])))
                            {
                                Directory.CreateDirectory(dictonaryPath + Convert.ToString(dr["ContractNo"]));
                            }



                            if (!File.Exists(dictonaryPath + fileDictionaryPath))
                                try
                                {
                                    client.DownloadFile(new Uri(Convert.ToString(dr["ContractsPath"])), dictonaryPath + fileDictionaryPath);
                                }
                                catch
                                {
                                    try
                                    {
                                        client.DownloadFile(new Uri(Convert.ToString(dr["ContractsPathSigned"])), dictonaryPath + fileDictionaryPath);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            client.DownloadFile(new Uri(Convert.ToString(dr["ContractsPathUploaded"])), dictonaryPath + fileDictionaryPath);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                        }
                    }
                }


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<string> invoies = new List<string>();
            invoies.Add("14th%20Jan%202022_Valley%20Vista%20Services,%20Inc0001_V97.pdf");
            invoies.Add("14th%20Jan%202022_Waste%20Management0001_V97.pdf");
            invoies.Add("INVC12012022000408-1592.pdf");
            invoies.Add("INVC12012022000408-1593.pdf");
            invoies.Add("INVC12012022000408-1594.pdf");
            invoies.Add("INVC12012022000408-1595.pdf");
            invoies.Add("INVC12012022000408-1596.pdf");
            invoies.Add("INVC12012022000408-1597.pdf");
            invoies.Add("INVC12012022000408-1598.pdf");
            invoies.Add("INVC12012022000408-1599.pdf");
            invoies.Add("INVC12012022000408-1600.pdf");
            invoies.Add("INVC12012022000408-1601.pdf");
            invoies.Add("INVC12012022000408-1602.pdf");
            invoies.Add("INVC12012022000408-1603.pdf");
            invoies.Add("INVC12012022000408-1604.pdf");
            invoies.Add("INVC12012022000408-1605.pdf");
            invoies.Add("INVC12012022000408-1606.pdf");
            invoies.Add("INVC12012022000408-1607.pdf");
            invoies.Add("INVC12012022000408-1608.pdf");
            invoies.Add("INVC12012022000408-1609.pdf");
            invoies.Add("INVC12012022000408-1610.pdf");
            invoies.Add("INVC12012022000408-1611.pdf");
            invoies.Add("INVC12012022000408-1612.pdf");
            invoies.Add("INVC12012022000408-1613.pdf");
            invoies.Add("INVC12012022000408-1614.pdf");
            invoies.Add("INVC12012022000408-1615.pdf");
            invoies.Add("INVC12012022000408-1616.pdf");
            invoies.Add("INVC12012022000408-1617.pdf");
            invoies.Add("INVC12012022000408-1618.pdf");
            invoies.Add("INVC12012022000408-1619.pdf");
            invoies.Add("INVC12012022000408-1620.pdf");
            invoies.Add("INVC12012022000408-1621.pdf");
            invoies.Add("INVC12012022000408-1622.pdf");
            invoies.Add("INVC12012022000408-1623.pdf");
            invoies.Add("INVC12012022000408-1624.pdf");
            invoies.Add("INVC12012022000408-1625.pdf");
            invoies.Add("INVC12012022000408-1626.pdf");
            invoies.Add("INVC12012022000408-1627.pdf");
            invoies.Add("INVC12012022000408-1628.pdf");
            invoies.Add("INVC12012022000408-1629.pdf");
            invoies.Add("INVC12012022000408-1630.pdf");
            invoies.Add("INVC12012022000408-1631.pdf");
            invoies.Add("INVC12012022000408-1632.pdf");
            invoies.Add("INVC12012022000408-1633.pdf");
            invoies.Add("INVC12012022000408-1634.pdf");
            invoies.Add("INVC12012022000408-1635.pdf");
            invoies.Add("INVC12012022000408-1636.pdf");
            invoies.Add("INVC12012022000408-1637.pdf");
            invoies.Add("INVC12012022000408-1638.pdf");
            invoies.Add("INVC12012022000408-1639.pdf");
            invoies.Add("INVC12012022000408-1640.pdf");
            invoies.Add("INVC12012022000408-1641.pdf");
            invoies.Add("INVC12012022000408-1642.pdf");
            invoies.Add("INVC12012022000408-1643.pdf");
            invoies.Add("INVC12012022000408-1644.pdf");
            invoies.Add("INVC12012022000408-1645.pdf");
            invoies.Add("INVC12012022000408-1646.pdf");
            invoies.Add("INVC12012022000408-1647.pdf");
            invoies.Add("INVC12012022000408-1648.pdf");
            invoies.Add("INVC12012022000408-1649.pdf");
            invoies.Add("INVC12012022000408-1650.pdf");
            invoies.Add("INVC12012022000408-1651.pdf");
            invoies.Add("INVC12012022000408-1652.pdf");
            invoies.Add("INVC12012022000408-1653.pdf");
            invoies.Add("INVC12012022000408-1654.pdf");
            invoies.Add("INVC12012022000408-1655.pdf");
            invoies.Add("INVC12012022000408-1656.pdf");
            invoies.Add("INVC12012022000408-1657.pdf");
            invoies.Add("INVC12012022000408-1658.pdf");
            invoies.Add("INVC12012022000408-1659.pdf");
            invoies.Add("INVC12012022000408-1660.pdf");
            invoies.Add("INVC12012022000408-1661.pdf");
            invoies.Add("INVC12012022000408-1662.pdf");
            invoies.Add("INVC12012022000408-1663.pdf");
            invoies.Add("INVC12012022000408-1664.pdf");
            invoies.Add("INVC12012022000408-1665.pdf");
            invoies.Add("INVC12012022000408-1666.pdf");
            invoies.Add("INVC12012022000408-1667.pdf");
            invoies.Add("INVC12012022000408-1668.pdf");
            invoies.Add("INVC12012022000408-1669.pdf");
            invoies.Add("INVC12012022000408-1670.pdf");
            invoies.Add("INVC12012022000408-1671.pdf");
            invoies.Add("INVC12012022000408-1672.pdf");
            invoies.Add("INVC12012022000408-1673.pdf");
            invoies.Add("INVC12012022000408-1674.pdf");
            invoies.Add("INVC12012022000408-1675.pdf");
            invoies.Add("INVC12012022000408-1676.pdf");
            invoies.Add("INVC12012022000408-1677.pdf");
            invoies.Add("INVC12012022000408-1678.pdf");
            invoies.Add("INVC12012022000408-1679.pdf");
            invoies.Add("INVC12012022000408-1680.pdf");
            invoies.Add("INVC12012022000408-1681.pdf");
            invoies.Add("INVC12012022000408-1682.pdf");
            invoies.Add("INVC12012022000408-1683.pdf");
            invoies.Add("INVC12012022000408-1684.pdf");
            invoies.Add("INVC12012022000408-1685.pdf");
            invoies.Add("INVC12012022000408-1686.pdf");
            invoies.Add("INVC12012022000408-1687.pdf");
            invoies.Add("INVC12012022000408-1688.pdf");
            invoies.Add("INVC12012022000408-1689.pdf");
            invoies.Add("INVC12012022000408-1690.pdf");
            invoies.Add("INVC12012022000408-1691.pdf");
            invoies.Add("INVC12012022000408-1692.pdf");
            invoies.Add("INVC12012022000408-1693.pdf");
            invoies.Add("INVC12012022000408-1694.pdf");
            invoies.Add("INVC12012022000408-1695.pdf");
            invoies.Add("INVC12012022000408-1696.pdf");
            invoies.Add("INVC12012022000408-1697.pdf");
            invoies.Add("INVC12012022000408-1698.pdf");
            invoies.Add("INVC12012022000408-1699.pdf");
            invoies.Add("INVC12012022000408-1700.pdf");
            invoies.Add("INVC12012022000408-1701.pdf");
            invoies.Add("INVC12012022000408-1702.pdf");
            invoies.Add("INVC12012022000408-1703.pdf");
            invoies.Add("INVC12012022000408-1704.pdf");
            invoies.Add("INVC12012022000408-1705.pdf");
            invoies.Add("INVC12012022000408-1706.pdf");
            invoies.Add("INVC12012022000408-1707.pdf");
            invoies.Add("INVC12012022000408-1708.pdf");
            invoies.Add("INVC12012022000408-1709.pdf");
            invoies.Add("INVC12012022000408-1710.pdf");
            invoies.Add("INVC12012022000408-1711.pdf");
            invoies.Add("INVC12012022000408-1712.pdf");
            invoies.Add("INVC12012022000408-1713.pdf");
            invoies.Add("INVC12012022000408-1714.pdf");
            invoies.Add("INVC12012022000408-1715.pdf");
            invoies.Add("INVC12012022000408-1716.pdf");
            invoies.Add("INVC12012022000408-1717.pdf");
            invoies.Add("INVC12012022000408-1718.pdf");
            invoies.Add("INVC12012022000408-1719.pdf");
            invoies.Add("INVC12012022000408-1720.pdf");
            invoies.Add("INVC12012022000408-1721.pdf");
            invoies.Add("INVC12012022000408-1722.pdf");
            invoies.Add("INVC12012022000408-1723.pdf");
            invoies.Add("INVC12012022000408-1724.pdf");
            invoies.Add("INVC12012022000408-1725.pdf");
            invoies.Add("INVC12012022000408-1726.pdf");
            invoies.Add("INVC12012022000408-1727.pdf");
            invoies.Add("INVC12012022000408-1728.pdf");
            invoies.Add("INVC12012022000408-1729.pdf");
            invoies.Add("INVC12012022000408-1730.pdf");
            invoies.Add("INVC12012022000408-1731.pdf");
            invoies.Add("INVC12012022000408-1732.pdf");
            invoies.Add("INVC12012022000408-1733.pdf");
            invoies.Add("INVC12012022000408-1734.pdf");
            invoies.Add("INVC12012022000408-1735.pdf");
            invoies.Add("INVC12012022000408-1736.pdf");
            invoies.Add("INVC12012022000408-1737.pdf");
            invoies.Add("INVC12012022000408-1738.pdf");
            invoies.Add("INVC12012022000408-1739.pdf");
            invoies.Add("INVC12012022000408-1740.pdf");
            invoies.Add("INVC12012022000408-1741.pdf");
            invoies.Add("INVC12012022000408-1742.pdf");
            invoies.Add("INVC12012022000408-1743.pdf");
            invoies.Add("INVC12012022000408-1744.pdf");
            invoies.Add("INVC12012022000408-1745.pdf");
            invoies.Add("INVC12012022000408-1746.pdf");
            invoies.Add("INVC12012022000408-1747.pdf");
            invoies.Add("INVC12012022000408-1748.pdf");
            invoies.Add("INVC12012022000408-1749.pdf");
            invoies.Add("INVC12012022000408-1750.pdf");
            invoies.Add("INVC12012022000408-1751.pdf");
            invoies.Add("INVC12012022000408-1752.pdf");
            invoies.Add("INVC12012022000408-1753.pdf");
            invoies.Add("INVC12012022000408-1754.pdf");
            invoies.Add("INVC12012022000408-1755.pdf");
            invoies.Add("INVC12012022000408-1756.pdf");
            invoies.Add("INVC12012022000408-1757.pdf");
            invoies.Add("INVC12012022000408-1758.pdf");
            invoies.Add("INVC12012022000408-1759.pdf");
            invoies.Add("INVC12012022000408-1760.pdf");
            invoies.Add("INVC12012022000408-1761.pdf");
            invoies.Add("INVC12012022000408-1762.pdf");
            invoies.Add("INVC12012022000408-1763.pdf");
            invoies.Add("INVC12012022000408-1764.pdf");
            invoies.Add("INVC12012022000408-1765.pdf");
            invoies.Add("INVC12012022000408-1766.pdf");
            invoies.Add("INVC12012022000408-1767.pdf");
            invoies.Add("INVC12012022000408-1768.pdf");
            invoies.Add("INVC12012022000408-1769.pdf");
            invoies.Add("INVC12012022000408-1770.pdf");
            invoies.Add("INVC12012022000408-1771.pdf");
            invoies.Add("INVC12012022000408-1772.pdf");
            invoies.Add("INVC12012022000408-1773.pdf");
            invoies.Add("INVC12012022000408-1774.pdf");
            invoies.Add("INVC12012022000408-1775.pdf");
            invoies.Add("INVC12012022000408-1776.pdf");
            invoies.Add("INVC12012022000408-1777.pdf");
            invoies.Add("INVC12012022000408-1778.pdf");
            invoies.Add("INVC12012022000408-1779.pdf");
            invoies.Add("INVC12012022000408-1780.pdf");
            invoies.Add("INVC12012022000408-1781.pdf");
            invoies.Add("INVC12012022000408-1782.pdf");
            invoies.Add("INVC12012022000408-1783.pdf");
            invoies.Add("INVC12012022000408-1784.pdf");
            invoies.Add("INVC12012022000408-1785.pdf");
            invoies.Add("INVC12012022000408-1786.pdf");
            invoies.Add("INVC12012022000408-1787.pdf");
            invoies.Add("INVC12012022000408-1788.pdf");
            invoies.Add("INVC12012022000408-1789.pdf");
            invoies.Add("INVC12012022000408-1790.pdf");
            invoies.Add("INVC12012022000408-1791.pdf");
            invoies.Add("INVC12012022000408-1792.pdf");
            invoies.Add("INVC12012022000408-1793.pdf");
            invoies.Add("INVC12012022000408-1794.pdf");
            invoies.Add("INVC12012022000408-1795.pdf");
            invoies.Add("INVC12012022000408-1796.pdf");
            invoies.Add("INVC12012022000408-1797.pdf");
            invoies.Add("INVC12012022000408-1798.pdf");
            invoies.Add("INVC12012022000408-1799.pdf");
            invoies.Add("INVC12012022000408-1800.pdf");
            invoies.Add("INVC12012022000408-1801.pdf");
            invoies.Add("INVC12012022000408-1802.pdf");
            invoies.Add("INVC12012022000408-1803.pdf");
            invoies.Add("INVC12012022000408-1804.pdf");
            invoies.Add("INVC12012022000408-1805.pdf");
            invoies.Add("INVC12012022000408-1806.pdf");
            invoies.Add("INVC12012022000408-1807.pdf");
            invoies.Add("INVC12012022000408-1808.pdf");
            invoies.Add("INVC12012022000408-1809.pdf");
            invoies.Add("INVC12012022000408-1810.pdf");
            invoies.Add("INVC12012022000408-1811.pdf");
            invoies.Add("INVC12012022000408-1812.pdf");
            invoies.Add("INVC12012022000408-1813.pdf");
            invoies.Add("INVC12012022000408-1814.pdf");
            invoies.Add("INVC12012022000408-1815.pdf");
            invoies.Add("INVC12012022000408-1816.pdf");
            invoies.Add("INVC12012022000408-1817.pdf");
            invoies.Add("INVC12012022000408-1818.pdf");
            invoies.Add("INVC12012022000408-1819.pdf");
            invoies.Add("INVC12012022000408-1820.pdf");
            invoies.Add("INVC12012022000408-1821.pdf");
            invoies.Add("INVC12012022000408-1822.pdf");
            invoies.Add("INVC12012022000408-1823.pdf");
            invoies.Add("INVC12012022000408-1824.pdf");
            invoies.Add("INVC12012022000408-1825.pdf");
            invoies.Add("INVC12012022000408-1826.pdf");
            invoies.Add("INVC12012022000408-1827.pdf");
            invoies.Add("INVC12012022000408-1828.pdf");
            invoies.Add("INVC12012022000408-1829.pdf");
            invoies.Add("INVC12012022000408-1830.pdf");
            invoies.Add("INVC12012022000408-1831.pdf");
            invoies.Add("INVC12012022000408-1832.pdf");
            invoies.Add("INVC12012022000408-1833.pdf");
            invoies.Add("INVC12012022000408-1834.pdf");
            invoies.Add("INVC12012022000408-1835.pdf");
            invoies.Add("INVC12012022000408-1836.pdf");
            invoies.Add("INVC12012022000408-1837.pdf");
            invoies.Add("INVC12012022000408-1838.pdf");
            invoies.Add("INVC12012022000408-1839.pdf");
            invoies.Add("INVC12012022000408-1840.pdf");
            invoies.Add("INVC12012022000408-1841.pdf");
            invoies.Add("INVC12012022000408-1842.pdf");
            invoies.Add("INVC12012022000408-1843.pdf");
            invoies.Add("INVC12012022000408-1844.pdf");
            invoies.Add("INVC12012022000408-1845.pdf");
            invoies.Add("INVC12012022000408-1846.pdf");
            invoies.Add("INVC12012022000408-1847.pdf");
            invoies.Add("INVC12012022000408-1848.pdf");
            invoies.Add("INVC12012022000408-1849.pdf");
            invoies.Add("INVC12012022000408-1850.pdf");
            invoies.Add("INVC12012022000408-1851.pdf");
            invoies.Add("INVC12012022000408-1852.pdf");
            invoies.Add("INVC12012022000408-1853.pdf");
            invoies.Add("INVC12012022000408-1854.pdf");
            invoies.Add("INVC12012022000408-1855.pdf");
            invoies.Add("INVC12012022000408-1856.pdf");
            invoies.Add("INVC12012022000408-1857.pdf");
            invoies.Add("INVC12012022000408-1858.pdf");
            invoies.Add("INVC12012022000408-1859.pdf");
            invoies.Add("INVC12012022000408-1860.pdf");
            invoies.Add("INVC12012022000408-1861.pdf");
            invoies.Add("INVC12012022000408-1862.pdf");
            invoies.Add("INVC12012022000408-1863.pdf");
            invoies.Add("INVC12012022000408-1864.pdf");
            invoies.Add("INVC12012022000408-1865.pdf");
            invoies.Add("INVC12012022000408-1866.pdf");
            invoies.Add("INVC12012022000408-1867.pdf");
            invoies.Add("INVC12012022000408-1868.pdf");
            invoies.Add("INVC12012022000408-1869.pdf");
            invoies.Add("INVC12012022000408-1870.pdf");
            invoies.Add("INVC12012022000408-1871.pdf");
            invoies.Add("INVC12012022000408-1872.pdf");
            invoies.Add("INVC12012022000408-1873.pdf");
            invoies.Add("INVC12012022000408-1874.pdf");
            invoies.Add("INVC12012022000408-1875.pdf");
            invoies.Add("INVC12012022000408-1876.pdf");
            invoies.Add("INVC12012022000408-1877.pdf");
            invoies.Add("INVC12012022000408-1878.pdf");
            invoies.Add("INVC12012022000408-1879.pdf");
            invoies.Add("INVC12012022000408-1880.pdf");
            invoies.Add("INVC12012022000408-1881.pdf");
            invoies.Add("INVC12012022000408-1882.pdf");
            invoies.Add("INVC12012022000408-1883.pdf");
            invoies.Add("INVC12012022000408-1884.pdf");
            invoies.Add("INVC12012022000408-1885.pdf");
            invoies.Add("INVC12012022000408-1886.pdf");
            invoies.Add("INVC12012022000408-1887.pdf");
            invoies.Add("INVC13012022054400-507.pdf");

            foreach(var itm in invoies)
            {
                string SoureFolder = @"C:\VectorFiles\BatchInvoices\3\" + itm.ToString();
                string DEScFolder = @"C:\VectorFiles\BatchInvoices\New3\" + itm.ToString();

                if (File.Exists(SoureFolder))
                    File.Move(SoureFolder, DEScFolder);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {




                using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
                {

                    String dictonaryPath = @"C:\VectorFiles\Client\";


                    DataSet DSData = objMasterDataBL.GetDownloadInfo("MissingClientDocuments", txtSearchText.Text);


                    foreach (DataRow dr in DSData.Tables[0].Rows)
                    {
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            using (System.Net.WebClient client = new System.Net.WebClient())
                            {

                                string fileDictionaryPath = Convert.ToString(dr["ClientNo"]) + "/" + Convert.ToString(dr["DocumentName"]);

                                if (!Directory.Exists(dictonaryPath + Convert.ToString(dr["ClientNo"])))
                                {
                                    Directory.CreateDirectory(dictonaryPath + Convert.ToString(dr["ClientNo"]));
                                }



                                if (!File.Exists(dictonaryPath + fileDictionaryPath))
                                    try
                                    {
                                        client.DownloadFile(new Uri(Convert.ToString(dr["WebUrl"])), dictonaryPath + fileDictionaryPath);
                                    }
                                    catch
                                    {

                                    }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {




                using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
                {

                    String dictonaryPath = @"C:\Srinivas\Vector\Documents\PRDocuments\Property\";


                    DataSet DSData = objMasterDataBL.GetDownloadInfo("PropertyNewDocs", txtSearchText.Text);

                    //foreach (DataRow dr in DSData.Tables[1].Rows)
                    //{

                    //        DirectoryInfo di = new DirectoryInfo(dictonaryPath + Convert.ToString(dr["BatchNo"]));
                    //        // Get a reference to each file in that directory.
                    //        FileInfo[] fiArr = di.GetFiles();

                    //        foreach (var file in fiArr)
                    //            if (file.Length == 0)
                    //            {
                    //                File.Delete(dictonaryPath + Convert.ToString(dr["BatchNo"]) + "/" + file.Name);
                    //            }

                    //}




                    foreach (DataRow dr in DSData.Tables[0].Rows)
                    {
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            using (System.Net.WebClient client = new System.Net.WebClient())
                            {

                                string fileDictionaryPath = Convert.ToString(dr["PropertyNo"]) + "/" + Convert.ToString(dr["DocumentName"]);

                                if (!Directory.Exists(dictonaryPath + Convert.ToString(dr["PropertyNo"])))
                                {
                                    Directory.CreateDirectory(dictonaryPath + Convert.ToString(dr["PropertyNo"]));
                                }



                                if (!File.Exists(dictonaryPath + fileDictionaryPath))
                                    try
                                    {
                                        client.DownloadFile(new Uri(Convert.ToString(dr["WebUrl"])), dictonaryPath + fileDictionaryPath);
                                    }
                                    catch
                                    {

                                    }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {

            }


        }
    }

}
