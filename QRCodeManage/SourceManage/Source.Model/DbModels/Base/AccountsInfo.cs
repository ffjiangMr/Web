using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Model.DbModels.Base
{
    [NotMapped]
    public class AccountsInfo
    {
        /// <summary>   
        /// 用户标识
        /// </summary>
        public int UserID { get; set; } 
        /// <summary>
        /// 游戏标识
        /// </summary>  
        public int GameID { get; set; }
        public int ProtectID { get; set; }
        public int PasswordID { get; set; }
        public int SpreaderID { get; set; }
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Accounts { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        public string RegAccounts { get; set; }
        public string UnderWrite { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PassPortID { get; set; }
        /// <summary>
        /// 真实名字
        /// </summary>
        public string Compellation { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LogonPass { get; set; }
        public string InsurePass { get; set; }
        public string DynamicPass { get; set; }
        public DateTime DynamicPassTime { get; set; }
        public short FaceID { get; set; }
        public int CustomID { get; set; }
        public int Present { get; set; }
        public int UserMedal { get; set; }
        /// <summary>
        /// 经验数值
        /// </summary>
        public int Experience { get; set; }
        public int GrowLevelID { get; set; }
        public int LoveLiness { get; set; }
        public int UserRight { get; set; }
        public int MasterRight { get; set; }
        public int ServiceRight { get; set; }
        public byte MasterOrder { get; set; }
        public byte MemberOrder { get; set; }
        public System.DateTime MemberOverDate { get; set; }
        public System.DateTime MemberSwitchDate { get; set; }
        public byte CustomFaceVer { get; set; }
        public byte Gender { get; set; }
        /// <summary>
        /// 禁止服务
        /// </summary>
        public byte Nullity { get; set; }
        /// <summary>
        /// 禁止时间
        /// </summary>
        public System.DateTime NullityOverDate { get; set; }
        public byte StunDown { get; set; }
        public byte MoorMachine { get; set; }
        /// <summary>
        /// 是否机器人
        /// </summary>
        public byte IsAndroid { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int WebLogonTimes { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int GameLogonTimes { get; set; }
        public int PlayTimeCount { get; set; }
        public int OnLineTimeCount { get; set; }
        public string LastLogonIP { get; set; }
        public System.DateTime LastLogonDate { get; set; }
        public string LastLogonMobile { get; set; }
        public string LastLogonMachine { get; set; }
        public string RegisterIP { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public string RegisterMobile { get; set; }
        public string RegisterMachine { get; set; }
        public Nullable<byte> RegisterOrigin { get; set; }
        public short PlatformID { get; set; }
        public string UserUin { get; set; }
        public Nullable<int> RankID { get; set; }
        public int AgentID { get; set; }
    }
}
