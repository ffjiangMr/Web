using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Admin.Web.Models.ApiParams
{
    public class ParamsGuIdNumber
    {
        public string GuIdNumber { get; set; }
    }
    /// <summary>
    /// 获取某一次分码信息
    /// </summary>
    public class ParamsGetUserAllocation: ParamsGuIdNumber
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// 手动装箱参数
    /// </summary>
    public class ParamsManualInsertNumBox : ParamsGuIdNumber
    {
        /// <summary>
        /// 所发码ID
        /// </summary>
        public int UAllocationId { get; set; }

        /// <summary>
        /// 开始码
        /// </summary>
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        public long EndNumber { get; set; }
        /// <summary>
        /// 码对应URL
        /// </summary>
        public string ValueURL { get; set; }
        /// <summary>
        /// 同一体系装箱标识
        /// </summary>
        public string Guid { get; set; }

    }

    /// <summary>
    /// 自动装箱参数
    /// </summary>
    public class ParamsAutomationInsertNumBox : ParamsGuIdNumber
    {
        /// <summary>
        /// 装箱数量
        /// </summary>
        public int NumBoxCount { get; set; }
        /// <summary>
        /// 同一体系装箱标识
        /// </summary>
        public string Guid { get; set; }

    }

    /// <summary>
    /// 手动装箱参数
    /// </summary>
    public class ParamsNumBoxList : ParamsGuIdNumber
    {
        /// <summary>
        /// 装箱Id
        /// </summary>
        public string NumBoxIds { get; set; }

        /// <summary>
        /// 同一体系装箱标识
        /// </summary>
        public string Guid { get; set; }

    }



    /// <summary>
    /// 手动设置内容
    /// </summary>
    public class ParamsManualInsertNum : ParamsGuIdNumber
    {
        /// <summary>
        /// 箱ID
        /// </summary>
        public int NumBoxId { get; set; }

        /// <summary>
        /// 开始码
        /// </summary>
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        public long EndNumber { get; set; }
        /// <summary>
        /// 码对应URL
        /// </summary>
        public string ValueURL { get; set; }

    }

    /// <summary>
    /// 自动设置码参数
    /// </summary>
    public class ParamsAutomationInsertNum : ParamsGuIdNumber
    {
        /// <summary>
        /// 箱IDs
        /// </summary>
        public string NumBoxIds { get; set; }

        /// <summary>
        /// 同一体系装箱标识
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 装箱数量
        /// </summary>
        public int NumBoxCount { get; set; }
        /// <summary>
        /// 码对应URL
        /// </summary>
        public string ValueURL { get; set; }

    }




    /// <summary>
    /// 自动装箱同时设置内容参数
    /// </summary>
    public class ParamsAutomationInsertNumBoxAndNumber : ParamsGuIdNumber
    {
        /// <summary>
        /// 装箱数量
        /// </summary>
        public int NumBoxCount { get; set; }


        /// <summary>
        /// 码对应URL
        /// </summary>
        public string ValueURL { get; set; }


        /// <summary>
        /// 同一体系装箱标识
        /// </summary>
        public string Guid { get; set; }

    }

    /// <summary>
    /// 获取excel文件参数
    /// </summary>
    public class ParamsGetNuBermExcel : ParamsGuIdNumber
    {

        /// <summary>
        /// 开始码
        /// </summary>
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        public long EndNumber { get; set; }

    }

    // <summary>
    /// 获取二维码图-文件参数
    /// </summary>
    public class ParamsGetNuBerm : ParamsGuIdNumber
    {

        /// <summary>
        /// 码
        /// </summary>
        public long Number { get; set; }
        

    }

    // <summary>
    /// 修改二维码内容参数
    /// </summary>
    public class ParamsUpdateNuBerm : ParamsGuIdNumber
    {

        /// <summary>
        /// 码
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// 码URL
        /// </summary>
        public string ValueURL { get; set; }


    }

    // <summary>
    /// 获取二维码图-文件参数
    /// </summary>
    public class ParamsSplitNuBerm : ParamsGuIdNumber
    {

        /// <summary>
        /// 开始码
        /// </summary>
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        public long EndNumber { get; set; }

        /// <summary>
        /// 码URL
        /// </summary>
        public string ValueURL { get; set; }


    }

    

}