using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Net;

namespace 腐烂国度2_MOD管理小工具
{
    public partial class Form : System.Windows.Forms.Form
    {
        readonly string PATH_Unzip_TEMP = Path.GetDirectoryName(Application.ExecutablePath) + @"\Temp\";
        readonly string PATH_Mods = Path.GetDirectoryName(Application.ExecutablePath) + @"\Mods\";
        readonly string PATH_XML = Path.GetDirectoryName(Application.ExecutablePath) + @"\Mods.xml";
        string PATH_Content;
        readonly string PATH_Error_Log = Path.GetDirectoryName(Application.ExecutablePath) + @"\Error.log";
        readonly string Line = Environment.NewLine;
        string J_VerInfo;// 软件版本号
        System.Threading.Mutex newMutex;
        public Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            newMutex = new System.Threading.Mutex(true, "d235851d-5428-4121-9ba3-ebbd1d4855b7", out bool WExist);
            if (WExist == false)
            {
                MessageBox.Show("请勿多开！", "注意：", MessageBoxButtons.OK, MessageBoxIcon.Information);//弹出提示信息
                Environment.Exit(0);
            }
            J_VerInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion.Replace(".0.0", "");
            this.Text = "腐烂国度2 MOD管理小工具 v" + J_VerInfo;
            ReadXML();
            if (checkBox_禁止检查更新.Checked == false)
            {
                Task.Run(() => Check_Updates(false));
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            String[] FileDrop_PATH = ((String[])e.Data.GetData(DataFormats.FileDrop));
            if (FileDrop_PATH.Length > 0)
            {
                pictureBox_loading.Visible = true; // 显示加载中GIF
                bool UnzipBool = Unzip(FileDrop_PATH[0], PATH_Unzip_TEMP + Path.GetFileNameWithoutExtension(FileDrop_PATH[0]));
                if (UnzipBool == true)
                {
                    PATH_Content = "";
                    Check_Content(PATH_Unzip_TEMP + Path.GetFileNameWithoutExtension(FileDrop_PATH[0]));
                    //Debug.Print(PATH_Content);
                    if (PATH_Content != "")
                    {
                        string New_PATH_2 = Move_Mods(PATH_Content, PATH_Mods + Path.GetFileNameWithoutExtension(FileDrop_PATH[0]));
                        Directory.Delete(PATH_Unzip_TEMP + Path.GetFileNameWithoutExtension(FileDrop_PATH[0]), true);
                        listView_MOD列表.BeginUpdate();   //数据更新，UI暂时挂起
                        listView_MOD列表.Items.Add(new ListViewItem(new string[] { Path.GetFileNameWithoutExtension(FileDrop_PATH[0]) + New_PATH_2, "未安装", DateTime.Now.ToString("G"), "" }));
                        listView_MOD列表.EndUpdate();  //结束数据处理，UI界面一次性绘制
                        StatusLabel_已载入数量.Text = "   已载入 MOD：" + listView_MOD列表.Items.Count.ToString() + " 个";
                        File_Index(PATH_Mods + Path.GetFileNameWithoutExtension(FileDrop_PATH[0]) + New_PATH_2);
                        WriteXML();
                    }
                    else
                    {
                        MessageBox.Show("在 MOD 文件中，未发现 Content 文件夹！", "错误：", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("解压 MOD 压缩包失败！请尝试重新打包压缩包为 .zip 格式！", "错误：", MessageBoxButtons.OK);
                }
                pictureBox_loading.Visible = false; // 显示加载中GIF
            }
        }
        // 解压缩 MOD 压缩包文件
        private bool Unzip(string PATH_Zip, string PATH_Temp_Mods)
        {
            //Debug.Print(DateTime.Now.ToString("mm-ss-ffff"));
            //Debug.Print(Path.GetExtension(PATH_Zip));
            if (Path.GetExtension(PATH_Zip) == ".zip")
            {
                using (var archive = ZipArchive.Open(PATH_Zip))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(PATH_Temp_Mods, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            else if (Path.GetExtension(PATH_Zip) == ".rar")
            {

                using (var archive = RarArchive.Open(PATH_Zip))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(PATH_Temp_Mods, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            else if (Path.GetExtension(PATH_Zip) == ".7z")
            {
                FileInfo File_Size = new FileInfo(PATH_Zip);
                Debug.Print(System.Convert.ToInt32(System.Math.Ceiling(File_Size.Length / 1024.0)).ToString());
                if (System.Convert.ToInt32(System.Math.Ceiling(File_Size.Length / 1024.0)) > 1024)
                {
                    if (MessageBox.Show("因软件解压缩库对 7z 格式支持较差，超过 1MB 大小的 .7z 压缩包解压速度会非常慢，造成软件卡死的假象，是否继续载入？" + Line + "强烈推荐大于 1MB 的 .7z 压缩文件自行解压并重新打包为 .zip 压缩格式！","提示：",MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return false;
                    }
                }
                using (var archive = SevenZipArchive.Open(PATH_Zip))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(PATH_Temp_Mods, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            else
            {
                MessageBox.Show("目前只支持压缩文件格式：.zip(推荐) .rar .7z，其他格式的请手动解压并重新压缩为 .zip 格式(强烈推荐)后重试。", "错误：", MessageBoxButtons.OK);
            }
            return true;
            //Debug.Print(DateTime.Now.ToString("mm-ss-ffff"));
        }
        // 生成文件索引
        private void File_Index(string PATH)
        {
            File_Index_List.Clear();
            if (File.Exists(PATH + @"\Index.txt"))
            {
                File.Delete(PATH + @"\Index.txt");
            }
            List<string> File_Index_List_2 = GetInfoPath(PATH, PATH.Length + 1);
            /*foreach (var item in File_Index_List)
            {
                var index = File_Index_List.IndexOf(item);
                Debug.Print($"循环第{index}次  -  取到对应的文件信息：{item}");
            }*/
            File.WriteAllLines(PATH + @"\Index.txt", File_Index_List_2);

        }

        private static List<string> File_Index_List = new List<string>();
        public static List<string> GetInfoPath(string PATH, int PATH_Length)
        {
            DirectoryInfo dir = new DirectoryInfo(PATH);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                File_Index_List.Add(f.FullName.Substring(PATH_Length));//添加文件的路径到列表
            }

            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                GetInfoPath(d.FullName, PATH_Length);
                //list.Add(d.FullName);//添加文件夹的路径到列表
            }
            return File_Index_List;
        }
        // 检查是否有 Content 文件夹
        private void Check_Content(string PATH)
        {
            DirectoryInfo Dir_Content = new DirectoryInfo(PATH);
            DirectoryInfo[] Dir_Content_2 = Dir_Content.GetDirectories();

            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo Dir_Content_3 in Dir_Content_2)
            {
                if (Dir_Content_3.Name == "Content")
                {
                    //Debug.Print(Dir_Content_3.FullName);
                    PATH_Content = Dir_Content_3.FullName;
                    break;
                }
                //Debug.Print(Dir_Content_3.FullName);
                Check_Content(Dir_Content_3.FullName);
                if (PATH_Content != "")
                {
                    break;
                }
            }
        }
        // 移动 MOD 文件到 Mods 文件夹
        private string Move_Mods(string Old_PATH,string New_PATH)
        {
            string New_PATH_2 = "";
            if (Directory.Exists(New_PATH))
            {
                New_PATH_2 = new Random().Next(1000).ToString();
            }
            Directory.Move(Old_PATH + @"\", New_PATH + New_PATH_2 + @"\");
            return New_PATH_2;
        }
        // 载入 XML 配置文件
        private void ReadXML()
        {
            string XML_Status;
            int XML_InstallNum = 0;
            if (File.Exists(PATH_XML))
            {
                XDocument XML_xDoc = XDocument.Load(PATH_XML);
                XElement XML_Root = XML_xDoc.Element("Root");
                XElement XML_Mods = XML_Root.Element("Mods");
                //Debug.Print(XML_Root.Elements().Count().ToString());
                listView_MOD列表.BeginUpdate();   //数据更新，UI暂时挂起
                for (int XML_Mods_Index = 0; XML_Mods_Index < XML_Mods.Elements().Count(); XML_Mods_Index++)
                {
                    //Debug.Print(XML_Root.Elements().ElementAt(XML_Root_Index).ToString());
                    XElement XML_Mods_1 = XML_Mods.Elements().ElementAt(XML_Mods_Index);
                    //Debug.Print(XML_Mods.Attribute("Name").Value.ToString());
                    if (XML_Mods_1.Attribute("Status").Value.ToString() == "false")
                    {
                        XML_Status = "未安装";
                    }
                    else
                    {
                        XML_Status = "已安装";
                        XML_InstallNum = ++XML_InstallNum;
                    }
                    listView_MOD列表.Items.Add(new ListViewItem(new string[] { XML_Mods_1.Attribute("Name").Value.ToString(), XML_Status, XML_Mods_1.Attribute("LoadTime").Value.ToString(), XML_Mods_1.Attribute("InstallTime").Value.ToString() }));
                }
                listView_MOD列表.EndUpdate();  //结束数据处理，UI界面一次性绘制
                StatusLabel_已载入数量.Text = "   已载入 MOD：" + listView_MOD列表.Items.Count.ToString() + " 个";
                StatusLabel_已安装数量.Text = "，已安装 MOD：" + XML_InstallNum.ToString() + " 个";
                XElement XML_Setting = XML_Root.Element("Setting");
                textBox_MOD安装位置.Text = XML_Setting.Element("ModsPath").Value;
                if (XML_Setting.Element("DisableUpdate") == null)
                {
                    checkBox_禁止检查更新.Checked = false;
                }
                else
                {
                    checkBox_禁止检查更新.Checked = Convert.ToBoolean(XML_Setting.Element("DisableUpdate").Value);
                }
            }
            else
            {
                if (Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + @"\StateOfDecay2\Saved"))
                {
                    textBox_MOD安装位置.Text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + @"\StateOfDecay2\Saved";
                }
            }
            if (!Directory.Exists(PATH_Mods))
            {
                Directory.CreateDirectory(PATH_Mods);
            }
            if (!Directory.Exists(PATH_Unzip_TEMP))
            {
                Directory.CreateDirectory(PATH_Unzip_TEMP);
            }
        }
        // 写出 XML 配置文件
        private void WriteXML()
        {
            bool XML_Status;
            XDocument XML_xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement XML_Root = new XElement("Root");
            XML_xDoc.Add(XML_Root);
            XElement XML_Mods = new XElement("Mods");
            XML_Root.Add(XML_Mods);
            for (int ListView_Index = 0; ListView_Index < listView_MOD列表.Items.Count; ListView_Index++)
            {
                //Debug.Print(ListView_Index.ToString());
                if (listView_MOD列表.Items[ListView_Index].SubItems[1].Text == "未安装")
                {
                    XML_Status = false;
                }
                else
                {
                    XML_Status = true;
                }
                //创建一个子节点，添加属性
                XElement XML_Mods_1 = new XElement("Mode" + ListView_Index.ToString(),
                    new XAttribute("Name", listView_MOD列表.Items[ListView_Index].SubItems[0].Text),
                    new XAttribute("Status", XML_Status),
                    new XAttribute("LoadTime", listView_MOD列表.Items[ListView_Index].SubItems[2].Text),
                    new XAttribute("InstallTime", listView_MOD列表.Items[ListView_Index].SubItems[3].Text)
                    );
                XML_Mods.Add(XML_Mods_1);
            }
            XElement XML_Setting = new XElement("Setting",
                new XElement("ModsPath", textBox_MOD安装位置.Text),
                new XElement("DisableUpdate", checkBox_禁止检查更新.Checked)
                );
            XML_Root.Add(XML_Setting);
            //保存xml文件
            XML_xDoc.Save(PATH_XML);
        }
        // 删除 MOD
        private void Delete_Mods(int Index)
        {
            if (Directory.Exists(PATH_Mods + listView_MOD列表.Items[Index].SubItems[0].Text))
            {
                Directory.Delete(PATH_Mods + listView_MOD列表.Items[Index].SubItems[0].Text, true);
            }
            listView_MOD列表.BeginUpdate();   //数据更新，UI暂时挂起
            listView_MOD列表.Items[Index].Remove();
            listView_MOD列表.EndUpdate();  //结束数据处理，UI界面一次性绘制
            StatusLabel_已载入数量.Text = "   已载入 MOD：" + listView_MOD列表.Items.Count.ToString() + " 个";
        }
        // 看看与哪个MOD冲突
        private List<string> Conflict_Checking(List<string> Index_Conflict_1)
        {
            List<string> Index_Conflict_3 = Index_Conflict_1;
            List<string> Index_Conflict_2 = new List<string>();
            for (int Temp_i = Index_Conflict_1.Count - 1; Temp_i >= 0; Temp_i--)
            {
                string Index_Item = Index_Conflict_1[Temp_i];
                //Debug.Print(Temp_i.ToString() + "  " + Index_Conflict_1.Count.ToString() + "  111  " + Index_Item);
                for (int GetInstallNum_Index = 0; GetInstallNum_Index < listView_MOD列表.Items.Count; GetInstallNum_Index++)
                {
                    if (listView_MOD列表.Items[GetInstallNum_Index].SubItems[1].Text == "已安装")
                    {
                        string File_Index = PATH_Mods + listView_MOD列表.Items[GetInstallNum_Index].SubItems[0].Text + @"\Index.txt";
                        if (File.Exists(File_Index))
                        {
                            List<string> Index = new List<string>(File.ReadAllLines(File_Index));
                            if (Index.Count > 0)
                            {
                                foreach (string Index_Item_2 in Index)
                                {
                                    if (Index_Item == Index_Item_2)
                                    {
                                        Index_Conflict_2.Add("冲突MOD：" + listView_MOD列表.Items[GetInstallNum_Index].SubItems[0].Text + "，冲突文件：" + Index_Item_2);
                                        //Debug.Print(Index_Conflict_3.Count.ToString() + "  222  " + Index_Item_2);
                                        if (Index_Conflict_3.Contains(Index_Item_2))
                                        {
                                            Index_Conflict_3.Remove(Index_Item_2);
                                        }
                                        //Debug.Print(Index_Conflict_3.Count.ToString() + "  333  " + Index_Item_2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Index_Conflict_3.Count > 0)
            {
                foreach (string Index_Item_3 in Index_Conflict_3)
                {
                        Index_Conflict_2.Add("冲突MOD：未知（可能与手动安装的 MOD 冲突），冲突文件：" + Index_Item_3);
                }
            }
            return Index_Conflict_2;
        }
        // 安装 MOD
        private bool Install_Mods(string NAME)
        {
            if (Get_Game_Status() == false)
            {
                string Install_Mods_PATH = textBox_MOD安装位置.Text + @"\Cooked\WindowsNoEditor\StateOfDecay2\Content";
                if (!Directory.Exists(Install_Mods_PATH))
                {
                    Directory.CreateDirectory(Install_Mods_PATH);
                }
                // 读入索引文件
                string File_Index = PATH_Mods + NAME + @"\Index.txt";

                if (File.Exists(File_Index))
                {
                    List<string> Index = new List<string>(File.ReadAllLines(File_Index));
                    if (Index.Count > 0)
                    {
                        List<string> Index_Conflict = new List<string>();
                        // 检查索引文件是否冲突
                        foreach (var Index_Item in Index)
                        {
                            if (File.Exists(Install_Mods_PATH + @"\" + Index_Item))
                            {
                                Index_Conflict.Add(Index_Item);
                                //Debug.Print(Index_Item);
                            }
                        }
                        if (Index_Conflict.Count == 0)
                        {
                            foreach (var Index_Item in Index)
                            {
                                if (!Directory.Exists(Path.GetDirectoryName(Install_Mods_PATH + @"\" + Index_Item)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(Install_Mods_PATH + @"\" + Index_Item));
                                }
                                File.Copy(PATH_Mods + NAME + @"\" + Index_Item, Install_Mods_PATH + @"\" + Index_Item);
                            }

                        }
                        else if (Index_Conflict.Count == Index.Count)
                        {
                            foreach (var Index_Item in Index)
                            {
                                //Debug.Print(Get_MD5(Install_Mods_PATH + @"\" + Index_Item) + "   " + Get_MD5(PATH_Mods + NAME + @"\" + Index_Item));
                                if (Get_MD5(Install_Mods_PATH + @"\" + Index_Item) != Get_MD5(PATH_Mods + NAME + @"\" + Index_Item))
                                {
                                    string Index_Conflict_Num = Index_Conflict.Count.ToString();
                                    List<string> Index_Conflict_3 = Conflict_Checking(Index_Conflict);
                                    Index_Conflict_3.Insert(0, "###  " + NAME + "  ###");
                                    Index_Conflict_3.Add("###  " + NAME + "  ###");
                                    File.WriteAllLines(PATH_Error_Log, Index_Conflict_3);
                                    MessageBox.Show("安装 MOD 失败！原因：有 " + Index_Conflict_Num + " 个文件与已安装 MOD 冲突！" + Line + Line + "冲突的文件及路径已写入到错误日志(本软件旁边)：" + Line + PATH_Error_Log, "错误：", MessageBoxButtons.OK);
                                    return false;
                                }
                            }
                            MessageBox.Show("该 MOD 已安装（经检查文件完全一致），修改状态为：已安装。", "提示：", MessageBoxButtons.OK);
                        }
                        else
                        {
                            string Index_Conflict_Num = Index_Conflict.Count.ToString();
                            List<string> Index_Conflict_3 = Conflict_Checking(Index_Conflict);
                            Index_Conflict_3.Insert(0, "###  " + NAME + "  ###");
                            Index_Conflict_3.Add("###  " + NAME + "  ###");
                            File.WriteAllLines(PATH_Error_Log, Index_Conflict_3);
                            MessageBox.Show("安装 MOD 失败！原因：有 " + Index_Conflict_Num + " 个文件与已安装 MOD 冲突！" + Line + Line + "冲突的文件及路径已写入到错误日志(本软件旁边)：" + Line + PATH_Error_Log, "错误：", MessageBoxButtons.OK);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("安装 MOD 失败！原因：索引文件内容有误，请检查！" + Line + "索引文件：" + Line + File_Index, "错误：", MessageBoxButtons.OK);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("安装 MOD 失败！原因：没有找到索引文件，请尝试删除并重新载入该 MOD！" + Line + "索引文件：" + Line + File_Index, "错误：", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("安装 MOD 失败！原因：游戏正在运行！", "错误：", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        // 卸载 MOD
        private bool Uninstall_Mods(string NAME)
        {
            if (Get_Game_Status() == false)
            {
                string Install_Mods_PATH = textBox_MOD安装位置.Text + @"\Cooked\WindowsNoEditor\StateOfDecay2\Content";
                if (!Directory.Exists(Install_Mods_PATH))
                {
                    MessageBox.Show("安装 MOD 失败！原因：没有找到 MOD 文件！", "错误：", MessageBoxButtons.OK);
                    return false;
                }
                // 读入索引文件
                string File_Index = PATH_Mods + NAME + @"\Index.txt";

                if (File.Exists(File_Index))
                {
                    List<string> Index = new List<string>(File.ReadAllLines(File_Index));
                    if (Index.Count > 0)
                    {
                        // 检查索引文件是否冲突
                        foreach (var Index_Item in Index)
                        {
                            //Debug.Print(Install_Mods_PATH + @"\" + Index_Item);
                            if (File.Exists(Install_Mods_PATH + @"\" + Index_Item))
                            {
                                File.Delete(Install_Mods_PATH + @"\" + Index_Item);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("卸载 MOD 失败！原因：索引文件内容有误，请检查！" + Line + "索引文件：" + Line + File_Index, "错误：", MessageBoxButtons.OK);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("卸载 MOD 失败！原因：没有找到索引文件，请尝试删除并重新载入该 MOD（不影响已安装的MOD）！" + Line + "索引文件：" + Line + File_Index, "错误：", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("卸载 MOD 失败！原因：游戏正在运行！", "错误：", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        // 重命名 MOD
        private void Rename_Mods(string Old_Name,string New_Name)
        {
            if (!Directory.Exists(PATH_Mods + New_Name))
            {
                Directory.Move(PATH_Mods + Old_Name, PATH_Mods + New_Name);
                if (Directory.Exists(PATH_Mods + New_Name))
                {
                    listView_MOD列表.Items[listView_MOD列表.FocusedItem.Index].SubItems[0].Text = New_Name;
                }
                else
                {
                    MessageBox.Show("重命名 MOD 失败！请检查！", "错误：", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("新名称已存在，请换个名称再试！", "错误：", MessageBoxButtons.OK);
            }
            
        }
        // 获取并更新已安装数量（状态栏）
        private void GetInstallNum()
        {
            int InstallNum = 0;
            for (int GetInstallNum_Index = 0; GetInstallNum_Index < listView_MOD列表.Items.Count; GetInstallNum_Index++)
            {
                if (listView_MOD列表.Items[GetInstallNum_Index].SubItems[1].Text == "已安装")
                {
                    InstallNum = ++InstallNum;
                }
            }
            StatusLabel_已安装数量.Text = "，已安装 MOD：" + InstallNum.ToString() + " 个";
        }
        // 弹出MOD列表 右键菜单
        private void ListView_MOD列表_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Debug.Print(listView1.FocusedItem.Index.ToString());
                if (listView_MOD列表.FocusedItem.Index >= 0)
                {
                    if (listView_MOD列表.Items[listView_MOD列表.FocusedItem.Index].SubItems[1].Text == "未安装")
                    {
                        Menu_安装卸载.Text = "安装";
                    }
                    else
                    {
                        Menu_安装卸载.Text = "卸载";
                    }
                    Menu_列表框右键菜单.Show(listView_MOD列表, e.Location);
                }
            }
        }
        // 右键菜单 安装卸载
        private void Menu_安装卸载_Click(object sender, EventArgs e)
        {
            if (Menu_安装卸载.Text == "安装")
            {
                int Temp_Index = listView_MOD列表.FocusedItem.Index;
                if (Install_Mods(listView_MOD列表.Items[Temp_Index].SubItems[0].Text) == true)
                {
                    listView_MOD列表.Items[Temp_Index].SubItems[1].Text = "已安装";
                    listView_MOD列表.Items[Temp_Index].SubItems[3].Text = DateTime.Now.ToString("G");
                    GetInstallNum();
                    WriteXML();
                }
            }
            else if (Menu_安装卸载.Text == "卸载")
            {
                int Temp_Index = listView_MOD列表.FocusedItem.Index;
                if (Uninstall_Mods(listView_MOD列表.Items[Temp_Index].SubItems[0].Text) == true)
                {
                    listView_MOD列表.Items[Temp_Index].SubItems[1].Text = "未安装";
                    listView_MOD列表.Items[Temp_Index].SubItems[3].Text = "";
                    GetInstallNum();
                    WriteXML();
                }
            }
        }
        // 右键菜单 删除
        private void Menu_删除_Click(object sender, EventArgs e)
        {
            if (listView_MOD列表.Items[listView_MOD列表.FocusedItem.Index].SubItems[1].Text == "未安装")
            {
                if (MessageBox.Show("确定要删除该 MOD 吗？","提示：",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Delete_Mods(listView_MOD列表.FocusedItem.Index);
                    GetInstallNum();
                    WriteXML();
                }
            }
            else
            {
                MessageBox.Show("该 MOD 已安装，请先卸载！", "提示：", MessageBoxButtons.OK);
            }
        }
        // 右键菜单 查看
        private void Menu_查看_Click(object sender, EventArgs e)
        {
            Process.Start('"' + PATH_Mods + listView_MOD列表.Items[listView_MOD列表.FocusedItem.Index].SubItems[0].Text + '"');
        }
        // 右键菜单 重命名
        private void Menu_重命名_Click(object sender, EventArgs e)
        {
            listView_MOD列表.SelectedItems[0].BeginEdit();
        }
        string listview_Rename_Old_Name;
        int listview_Rename_Old_Index;
        // 重命名 MOD 前
        private void ListView_MOD列表_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            listview_Rename_Old_Index = listView_MOD列表.FocusedItem.Index;
            listview_Rename_Old_Name = listView_MOD列表.Items[listview_Rename_Old_Index].SubItems[0].Text;
            //Debug.Print(listview_Rename_Old_Name);
        }
        // 重命名 MOD 后
        private void ListView_MOD列表_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //Debug.Print(e.Label + "  " + listview_Rename_Old_Name);
            if (e.Label != "")
            {
                if (e.Label != listview_Rename_Old_Name)
                {
                    Rename_Mods(listview_Rename_Old_Name, e.Label);
                    WriteXML();
                }
            }
            //listView_MOD列表.SelectedItems[listview_Rename_Old_Index].BeginEdit()
        }
        // 浏览MOD安装位置
        private void Button_浏览MOD安装位置_Click(object sender, EventArgs e)
        {
            string LocalApplicationData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string Content_PATH = LocalApplicationData + @"\StateOfDecay2\Saved";
            //Debug.Print(Saved_Folder);
            while (!Directory.Exists(Content_PATH))
            {
                Content_PATH = Content_PATH.Substring(0, Content_PATH.LastIndexOf(@"\"));
                //Debug.Print(Content_PATH);
            }
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                SelectedPath = Content_PATH,
                Description = "请选择 [腐烂国度2] 游戏数据位置：" + Line + @"C:\Users\用户名\AppData\Local\StateOfDecay2\Saved"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_MOD安装位置.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        // 获取游戏运行状态
        private bool Get_Game_Status()
        {
            if (Process.GetProcessesByName("StateOfDecay2-Win64-Shipping").Length > 0)
            {
                return true;
            }
            return false;
        }
        // 手动检查更新
        private void StatusLabel_检查更新_Click(object sender, EventArgs e)
        {
            Task.Run(() => Check_Updates(true));
        }
        // 检查更新
        private void Check_Updates(bool Tipprompt)
        {
            string strHTML = Get_HTTP("https://api.xiuer.pw/ver/flgd2modglxgj.txt");
            string[] Ver_Info = strHTML.Split('\n');
            if (Ver_Info.Length > 2)
            {
                if (Ver_Info[1] != "")
                {
                    if (Ver_Info[1] != J_VerInfo)
                    {
                        if (MessageBox.Show("发现新版本 [v" + Ver_Info[1] + "]！是否前往更新？", "发现新版本！", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Process.Start(Ver_Info[2]);
                        }
                    }
                    else
                    {
                        if (Tipprompt == true)
                        {
                            MessageBox.Show("当前已是最新版本 " + J_VerInfo + " ！", "信息：", MessageBoxButtons.OK);
                        }

                    }
                }
                else
                {
                    if (Tipprompt == true)
                    {
                        MessageBox.Show("当前已是最新版本 " + J_VerInfo + " ！", "信息：", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                if (Tipprompt == true)
                {
                    MessageBox.Show("当前已是最新版本 " + J_VerInfo + " ！", "信息：", MessageBoxButtons.OK);
                }
            }
        }
        private string Get_HTTP(string url)
        {
            NewWebClient myWebClient = new NewWebClient(10000);
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            string strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        private string Get_MD5(string PATH)
        {
            var HASH_MD5 = System.Security.Cryptography.MD5.Create();
            var STREAM_MD5 = new FileStream(PATH, FileMode.Open);
            byte[] HASH_MD5_Byte = HASH_MD5.ComputeHash(STREAM_MD5);
            STREAM_MD5.Close();
            return BitConverter.ToString(HASH_MD5_Byte).Replace("-", "");
        }
        private void CheckBox_禁止检查更新_CheckedChanged(object sender, EventArgs e)
        {
            WriteXML();
        }
    }
    // 带超时时间的 WebClient
    public class NewWebClient : WebClient
    {
        private int _timeout;

        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public NewWebClient()
        {
            this._timeout = 60000;
        }

        public NewWebClient(int timeout)
        {
            this._timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
