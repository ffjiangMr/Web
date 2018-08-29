using System.ComponentModel;
using System.Web.Http;
using Source.Admin.Web.Filters;
using Source.Admin.Web.Models.ApiParams;
using Source.Core.Contracts.QRCode;
using TomNet.Web.Mvc.UI;
using TomNet.Web.WebApi;
using System.Linq;
using System;
using Source.Model.DbModels.QRCode;
using System.Collections.Generic;
using Source.Admin.Web.Common;
using System.Data;
using Source.Core.Contracts.Base;
using Source.Admin.Web.Models.ViewModel;
using Source.Model.DbModels.Base;

namespace Source.Admin.Web.Controllers.Api
{

    [Description("API-活码服务")]
    [ApiAuthentication]
    public class QRCodeController : BaseApiController
    {
        public IBaseDictionariesInfoContract DictionariesContract { get; set; }
        public IUserAccountContract UserAccountContract { get; set; }

        public IUserAllocationContract UserAllocationContract { get; set; }

        public IUserNumBoxContract UserNumBoxContract { get; set; }


        public IUserNumberContract UserNumberContract { get; set; }

        public IAPILogInfoContract APILogInfoContract { get; set; }

        #region 账户部分

        /// <summary>
        /// 申请开通账户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult Register(RegisterModel entity)
        {

            if (entity == null)
            {
                var result = new AjaxResult("传输参数错误", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "Register", "传输参数错误");
                return Token(result, "");
            }
            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                var result = new AjaxResult("应用名称不能为空", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "Register", "应用名称不能为空");
                return Token(result, "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                var result = new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "Register", "协约唯一标识不能为空");
                return Token(result, "");
            }

            if (UserAccountContract.Entities.Where(d => d.UserName == entity.UserName).Count() > 0)
            {
                var result = new AjaxResult("应用名称重复", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "Register", "应用名称重复");
                return Token(result, "");
            }

            if (UserAccountContract.Entities.Where(d => d.GuIdNumber == entity.GuIdNumber).Count() > 0)
            {
                var result = new AjaxResult("协约唯一标识重复", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "Register", "协约唯一标识重复");
                return Token(result, "");
            }

            try
            {
                var rel = UserAccountContract.Insert(new UserAccount
                {
                    UserName = entity.UserName,
                    CreatedTime = DateTime.Now,
                    UserStare = 1,
                    Password = "000000",
                    GuIdNumber = entity.GuIdNumber,
                    Position = entity.Position,
                    Phone = entity.Phone,
                    Explain = entity.Explain

                }) > 0;
                if (rel)
                {
                    var result = new AjaxResult("申请成功", AjaxResultType.Success);
                    InserLog(entity.GuIdNumber, "Register", "申请成功");
                    return Token(result, "");
                }
                else
                {
                    var result = new AjaxResult("申请失败", AjaxResultType.Error);
                    InserLog(entity.GuIdNumber, "Register", "申请失败");
                    return Token(result, "");
                }
            }
            catch (Exception e)
            {
                var result = new AjaxResult("申请失败" + e.Message, AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "Register", "申请失败");
                return Token(result, "");
            }
        }

        /// <summary>
        /// 验证账户是否开通
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult VerUserAccount(ParamsGuIdNumber entity)
        {

            if (entity == null)
            {
                var result = new AjaxResult("传输参数错误", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "VerUserAccount", "传输参数错误");
                return Token(result, "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                var result = new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "VerUserAccount", "协约唯一标识不能为空");
                return Token(result, "");
            }

            try
            {
                if (UserAccountContract.Entities.Where(d => d.GuIdNumber == entity.GuIdNumber).Select(de => de.UserStare).FirstOrDefault() == Convert.ToInt32(UserStare.通过))
                {
                    var result = new AjaxResult("账户已经开通", AjaxResultType.Success);
                    InserLog(entity.GuIdNumber, "VerUserAccount", "账户已经开通");
                    return Token(result, "");
                }
                else
                {
                    var result = new AjaxResult("账户没有开通,或者已经被禁用", AjaxResultType.Error);
                    InserLog(entity.GuIdNumber, "VerUserAccount", "账户没有开通,或者已经被禁用");
                    return Token(result, "");
                }
            }
            catch (Exception)
            {
                var result = new AjaxResult("请求失败", AjaxResultType.Error);
                InserLog(entity.GuIdNumber, "VerUserAccount", "请求失败");
                return Token(result, "");
            }
        }

        /// <summary>
        /// 验证账户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool VerificationUserAccount(UserAccount data)
        {
            //var data = UserAccountContract.Entities.Where(d => d.IsDeleted==false && d.GuIdNumber == GuIdNumber).FirstOrDefault();

            if (data == null) return true;

            return data.UserStare == Convert.ToInt32(UserStare.通过) ? false : true;
        }


        #endregion


        #region 发码部分接口


        /// <summary>
        /// 获取所有分码信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserAllocationList(ParamsGuIdNumber entity)
        {

            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserAllocationList", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserAllocationList", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserAllocationList", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }


                var datalist = (from t in (from uc in (UserAllocationContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false))
                                           join ub in UserNumBoxContract.Entities.Where(d => d.IsDeleted == false) on uc.Id equals ub.UAllocationId
                                           into temp
                                           from tt in temp.DefaultIfEmpty()
                                           select new
                                           {
                                               uc.Id,
                                               uc.StartNumber,
                                               uc.EndNumber,
                                               uc.NumCount,
                                               uc.AllocationStare,
                                               UseCount = tt.NumCount,
                                               uc.CreatedTime
                                           })
                                group t by t.Id into g
                                select new
                                {
                                    id = g.Key,
                                    StartNumber = g.Max(p => p.StartNumber),
                                    EndNumber = g.Max(p => p.EndNumber),
                                    NumCount = g.Max(p => p.NumCount),
                                    AllocationStare = g.Max(p => p.AllocationStare),
                                    CreatedTime = g.Max(p => p.CreatedTime),
                                    UseCount = g.Sum(p => (p.UseCount == null) ? 0 : p.UseCount),
                                }).ToList();

                InserLog(entity.GuIdNumber, "UserAllocationList", "获取成功");
                return Token(new AjaxResult("获取成功", AjaxResultType.Success, datalist), "");

            }
            catch (Exception)
            {
                InserLog(entity.GuIdNumber, "UserAllocationList", "获取失败");
                return Token(new AjaxResult("获取失败", AjaxResultType.Error), "");
            }
        }

        /// <summary>
        /// 获取分码信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserAllocation(ParamsGetUserAllocation entity)
        {

            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserAllocation", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserAllocation", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserAllocation", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                var data = UserAllocationContract.Entities.Where(d => d.Id == entity.Id).FirstOrDefault();
                if (data == null)
                {
                    InserLog(entity.GuIdNumber, "UserAllocation", "获取失败");
                    return Token(new AjaxResult("获取失败", AjaxResultType.Error), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "UserAllocation", "获取成功");
                    return Token(new AjaxResult("获取成功", AjaxResultType.Success, data), "");
                }
            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserAllocation", "获取失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }



        }


        #endregion


        #region 装箱过程

        /// <summary>
        /// 手动装箱时--根据分码号，进行选择后获取可用最大码区间
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult ManualUserNumBoxGetnumber(ParamsGetUserAllocation entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                //匹配分配
                var dataUall = UserAllocationContract.Entities.Where(d => d.Id == entity.Id && d.IsDeleted == false).FirstOrDefault();
                if (dataUall == null)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "找不到分配码");
                    return Token(new AjaxResult("找不到分配码", AjaxResultType.Error), "");
                }
                //获取已经使用的码
                var databox = UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UAllocationId == entity.Id && d.UaId == dataAccount.Id);
                ReturnDataBoxGetnumber rdata;
                if (databox.Count() > 0)
                {
                    if (dataUall.NumCount == databox.Sum(d => d.NumCount))
                    {
                        InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "当前分码区间已经全部用完");
                        return Token(new AjaxResult("当前分码区间已经全部用完", AjaxResultType.Error), "");
                    }
                    rdata = new ReturnDataBoxGetnumber()
                    {
                        EndNumber = dataUall.EndNumber,
                        StartNumber = databox.Max(d => d.EndNumber),
                        NumCount = dataUall.EndNumber - databox.Max(d => d.EndNumber) + 1
                    };
                }
                else
                {
                    rdata = new ReturnDataBoxGetnumber()
                    {
                        EndNumber = dataUall.EndNumber,
                        NumCount = dataUall.NumCount,
                        StartNumber = dataUall.StartNumber


                    };
                }
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "获取成功");
                return Token(new AjaxResult("获取成功", AjaxResultType.Success, rdata), "");
            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }

        }

        /// <summary>
        /// 手动装箱
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult ManualUserNumBoxInsert(ParamsManualInsertNumBox entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                //匹配分配
                var dataUall = UserAllocationContract.Entities.Where(d => d.Id == entity.UAllocationId && d.IsDeleted == false).FirstOrDefault();
                if (dataUall == null)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "找不到分配码");
                    return Token(new AjaxResult("找不到分配码", AjaxResultType.Error), "");
                }

                if (dataUall.StartNumber > entity.StartNumber || dataUall.EndNumber < entity.EndNumber)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "您输入的码与分码信息不匹配");
                    return Token(new AjaxResult("您输入的码与分码信息不匹配", AjaxResultType.Error), "");
                }
                var databox = UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UAllocationId == entity.UAllocationId && d.UaId == dataAccount.Id);

                //判断使用
                var isok = databox.Where(d => (d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.StartNumber) ||
                   (d.StartNumber <= entity.EndNumber && d.EndNumber >= entity.EndNumber) || (d.StartNumber >= entity.StartNumber && d.StartNumber <= entity.EndNumber) ||
                   (d.EndNumber >= entity.StartNumber && d.EndNumber <= entity.EndNumber)).Count() > 0;

                if (isok)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "您输入的码已经被使用");
                    return Token(new AjaxResult("您输入的码已经被使用", AjaxResultType.Error), "");
                }
                var data = new UserNumBox
                {
                    UaId = dataAccount.Id,
                    UAllocationId = entity.UAllocationId,
                    NumBoxStart = Convert.ToInt32(NumBoxStart.正常),
                    CreatedTime = DateTime.Now,
                    StartNumber = entity.StartNumber,
                    EndNumber = entity.EndNumber,
                    NumCount = Convert.ToInt32(entity.EndNumber - entity.StartNumber + 1),
                    Guid = entity.Guid

                };

                var count = UserNumBoxContract.Insert(data);
                if (count > 0)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "设置成功");
                    return Token(new AjaxResult("设置成功", AjaxResultType.Success, data), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "设置失败");
                    return Token(new AjaxResult("设置失败", AjaxResultType.Error), "");
                }




            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "ManualUserNumBoxGetnumber", "设置失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }

        /// <summary>
        /// 自动化装箱
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult AutomationUserNumBoxInsert(ParamsAutomationInsertNumBox entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                var dataAll = UserAllocationContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false);
                if (dataAll.Count() == 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "账户没分配任何活码");
                    return Token(new AjaxResult("账户没分配任何活码", AjaxResultType.Error), "");
                }
                var databoxs = UserNumBoxContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false);
                if (dataAll.Sum(d => d.NumCount) - databoxs.Sum(d => d.NumCount) - entity.NumBoxCount < 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "账户没分配活码已经不足");
                    return Token(new AjaxResult("账户没分配活码已经不足", AjaxResultType.Error), "");
                }
                var dataAllList = dataAll.OrderBy(d => d.StartNumber).ToList();
                //循环载入
                List<UserNumBox> list = new List<UserNumBox>();
                var count = entity.NumBoxCount;
                foreach (var item in dataAllList)
                {
                    if (count <= 0) break;

                    var databox = UserNumBoxContract.Entities.Where(d => d.UAllocationId == item.Id && d.IsDeleted == false);
                    if (databox.Count() > 0 && databox.Sum(d => d.NumCount) >= item.NumCount) continue;

                    UserNumBox data = new UserNumBox();

                    data.NumBoxStart = Convert.ToInt32(NumBoxStart.正常);
                    data.CreatedTime = DateTime.Now;
                    if (databox.Count() > 0)
                        data.StartNumber = databox.Max(d => d.EndNumber) + 1;
                    else
                        data.StartNumber = item.StartNumber;
                    data.UaId = item.UaId;
                    data.UAllocationId = item.Id;
                    data.Guid = entity.Guid;
                    if (item.EndNumber - data.StartNumber + 1 >= count)
                    {
                        data.NumCount = count;
                        data.EndNumber = data.StartNumber + count - 1;
                        count = count - data.NumCount;
                    }
                    else
                    {
                        data.NumCount = Convert.ToInt32(item.EndNumber - data.StartNumber + 1);
                        data.EndNumber = data.StartNumber + data.NumCount - 1;
                        count = count - data.NumCount;
                    }
                    list.Add(data);

                }

                foreach (var item in list)
                {
                    var dcount = UserNumBoxContract.Insert(item);
                }

                if (list.Count > 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "设置成功");
                    return Token(new AjaxResult("设置成功", AjaxResultType.Success, list), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "设置成功");
                    return Token(new AjaxResult("设置成功", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumBoxInsert", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }

        /// <summary>
        /// 获取装箱信息--顺序给出可用最大码信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumBoxList(ParamsNumBoxList entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumBoxList", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumBoxList", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumBoxList", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }
                if (string.IsNullOrWhiteSpace(entity.NumBoxIds) && string.IsNullOrWhiteSpace(entity.Guid))
                {
                    InserLog(entity.GuIdNumber, "UserNumBoxList", "请输入箱码标识");
                    return Token(new AjaxResult("请输入箱码标识", AjaxResultType.Error), "");
                }
                if (string.IsNullOrWhiteSpace(entity.NumBoxIds))
                {
                    entity.NumBoxIds = "";
                }

                string[] idsStr = entity.NumBoxIds.Split(new char[] { ',' });
                int[] idsInt = entity.NumBoxIds == "" ? null : Array.ConvertAll(idsStr, id => Convert.ToInt32(id));

                var qUserNumBox = entity.NumBoxIds == "" ? UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UaId == dataAccount.Id && d.Guid == entity.Guid) : UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && idsInt.Contains(d.Id));


                var list = from t in (from uc in qUserNumBox
                                      join ub in UserNumberContract.Entities.Where(d => d.IsDeleted == false) on uc.Id equals ub.UNumBoxId
                                            into temp
                                      from tt in temp.DefaultIfEmpty()
                                      select new
                                      {
                                          uc.Id,
                                          uc.UaId,
                                          uc.UAllocationId,
                                          uc.StartNumber,
                                          uc.EndNumber,
                                          uc.NumCount,
                                          uc.NumBoxStart,
                                          uc.ValueURL,
                                          UseMaxNumber = tt.EndNumber,
                                          uc.CreatedTime
                                      })
                           group t by t.Id into g
                           select new
                           {
                               Id = g.Key,
                               StartNumber = g.Max(p => p.StartNumber),
                               UaId = g.Max(p => p.UaId),
                               UAllocationId = g.Max(p => p.UAllocationId),
                               EndNumber = g.Max(p => p.EndNumber),
                               NumCount = g.Max(p => p.NumCount),
                               NumBoxStart = g.Max(p => p.NumBoxStart),
                               CreatedTime = g.Max(p => p.CreatedTime),
                               UseMaxNumber = g.Max(p => (p.UseMaxNumber == null ? p.StartNumber : p.UseMaxNumber + 1)),
                               ValueURL = g.Max(p => p.ValueURL)
                           };
                InserLog(entity.GuIdNumber, "UserNumBoxList", "获取成功");
                return Token(new AjaxResult("获取成功", AjaxResultType.Success, list.ToList()), "");
            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumBoxList", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }




        #endregion


        #region 设置内容


        /// <summary>
        /// 手动通过码设置内容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult ManualUserNumberInsert(ParamsManualInsertNum entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                //获取所有箱信息
                var list = UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.Id == entity.NumBoxId);
                if (list.Count() == 0)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "没找到装箱信息");
                    return Token(new AjaxResult("没找到装箱信息", AjaxResultType.Error), "");
                }

                //检测设置区间是否在箱码内
                var dbox = list.Where(d => d.IsDeleted == false && d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.EndNumber).FirstOrDefault();
                if (dbox == null)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "装箱信息不包含这个区间码");
                    return Token(new AjaxResult("装箱信息不包含这个区间码", AjaxResultType.Error), "");
                }

                //检测区间是否被使用
                if (UserNumberContract.Entities.Where(d => d.IsDeleted == false && ((d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.StartNumber) ||
                 (d.StartNumber <= entity.EndNumber && d.EndNumber >= entity.EndNumber))).Count() > 0)
                {
                    InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "这个区间码已经被设置了内容");
                    return Token(new AjaxResult("这个区间码已经被设置了内容", AjaxResultType.Error), "");
                }

                var data = new UserNumber()
                {
                    UaId = dataAccount.Id,
                    UNumBoxId = dbox.Id,
                    CreatedTime = DateTime.Now,
                    StartNumber = entity.StartNumber,
                    EndNumber = entity.EndNumber,
                    GroupGUID = Guid.NewGuid().ToString(),
                    ValueURL = entity.ValueURL,
                    NumCount = Convert.ToInt32(entity.EndNumber - entity.StartNumber + 1)
                };

                UserNumberContract.Insert(data);
                InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "设置成功");
                return Token(new AjaxResult("设置成功", AjaxResultType.Success, data), "");
            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "ManualUserNumberInsert", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }

        /// <summary>
        /// 自动填充设置内容，返回码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult AutomationUserNumberInsert(ParamsAutomationInsertNum entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }


                //设置本次填充的分组标识
                var GroupGUID = Guid.NewGuid().ToString();

                if (string.IsNullOrWhiteSpace(entity.NumBoxIds) && string.IsNullOrWhiteSpace(entity.Guid))
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "请输入箱码标识");
                    return Token(new AjaxResult("请输入箱码标识", AjaxResultType.Error), "");
                }
                if (string.IsNullOrWhiteSpace(entity.NumBoxIds))
                {
                    entity.NumBoxIds = "";
                }

                string[] idsStr = entity.NumBoxIds.Split(new char[] { ',' });
                int[] idsInt = entity.NumBoxIds == "" ? null : Array.ConvertAll(idsStr, id => Convert.ToInt32(id));

                var list = entity.NumBoxIds == "" ? UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UaId == dataAccount.Id && d.Guid == entity.Guid) : UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && idsInt.Contains(d.Id));
                //所有箱码IDList
                var idslsitInt = list.Select(d => d.Id).ToList();

                if (list.Count() == 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "没找到装箱信息");
                    return Token(new AjaxResult("没找到装箱信息", AjaxResultType.Error), "");
                }

                var numlist = UserNumberContract.Entities.Where(d => d.IsDeleted == false && idslsitInt.Contains(d.UNumBoxId));

                if (list.Sum(d => d.NumCount) < (numlist.Sum(d => d.NumCount) + entity.NumBoxCount))
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "箱码不足，请及时补充");
                    return Token(new AjaxResult("箱码不足，请及时补充", AjaxResultType.Error), "");
                }

                List<UserNumber> insertlist = new List<UserNumber>();
                var count = entity.NumBoxCount;
                foreach (var item in list.ToList())
                {
                    if (count <= 0) break;

                    var datanum = UserNumberContract.Entities.Where(d => d.UNumBoxId == item.Id && d.IsDeleted == false);
                    if (datanum.Count() > 0 && datanum.Sum(d => d.NumCount) >= item.NumCount) continue;

                    UserNumber data = new UserNumber();

                    data.CreatedTime = DateTime.Now;
                    data.ValueURL = entity.ValueURL;
                    data.GroupGUID = GroupGUID;
                    if (datanum.Count() > 0)
                        data.StartNumber = datanum.Max(d => d.EndNumber) + 1;
                    else
                        data.StartNumber = item.StartNumber;
                    data.UaId = item.UaId;
                    data.UNumBoxId = item.Id;
                    if (item.EndNumber - data.StartNumber + 1 >= count)
                    {
                        data.NumCount = count;
                        data.EndNumber = data.StartNumber + count - 1;
                        count = count - data.NumCount;
                    }
                    else
                    {
                        data.NumCount = Convert.ToInt32(item.EndNumber - data.StartNumber + 1);
                        data.EndNumber = data.StartNumber + data.NumCount - 1;
                        count = count - data.NumCount;
                    }
                    insertlist.Add(data);

                }
                foreach (var item in insertlist)
                {
                    UserNumberContract.Insert(item);
                }

                if (insertlist.Count > 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "设置成功");
                    return Token(new AjaxResult("设置成功", AjaxResultType.Success, insertlist), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "设置失败");
                    return Token(new AjaxResult("设置失败", AjaxResultType.Error), "");
                }



            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumberInsert", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }





        #endregion


        #region 自动装箱-自动设置内容


        [AllowAnonymous]
        public IHttpActionResult AutomationUserNumBoxAndNumberInsert(ParamsAutomationInsertNumBoxAndNumber entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }

            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                var dataAll = UserAllocationContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false);
                if (dataAll.Count() == 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "账户没分配任何活码");
                    return Token(new AjaxResult("账户没分配任何活码", AjaxResultType.Error), "");
                }
                var databoxs = UserNumBoxContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false);
                if (dataAll.Sum(d => d.NumCount) - databoxs.Sum(d => d.NumCount) - entity.NumBoxCount < 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "账户没分配活码已经不足");
                    return Token(new AjaxResult("账户没分配活码已经不足", AjaxResultType.Error), "");
                }

                //设置本次填充的分组标识
                var GroupGUID = Guid.NewGuid().ToString();


                var dataAllList = dataAll.OrderBy(d => d.StartNumber).ToList();
                //循环载入
                List<UserNumBox> listbox = new List<UserNumBox>();
                var count = entity.NumBoxCount;
                foreach (var item in dataAllList)
                {
                    if (count <= 0) break;

                    var databox = UserNumBoxContract.Entities.Where(d => d.UAllocationId == item.Id && d.IsDeleted == false);
                    if (databox.Count() > 0 && databox.Sum(d => d.NumCount) >= item.NumCount) continue;

                    UserNumBox data = new UserNumBox();

                    data.NumBoxStart = Convert.ToInt32(NumBoxStart.正常);
                    data.CreatedTime = DateTime.Now;
                    data.Guid = entity.Guid;
                    if (databox.Count() > 0)
                        data.StartNumber = databox.Max(d => d.EndNumber) + 1;
                    else
                        data.StartNumber = item.StartNumber;
                    data.UaId = item.UaId;
                    data.UAllocationId = item.Id;
                    if (item.EndNumber - data.StartNumber + 1 >= count)
                    {
                        data.NumCount = count;
                        data.EndNumber = data.StartNumber + count - 1;
                        count = count - data.NumCount;
                    }
                    else
                    {
                        data.NumCount = Convert.ToInt32(item.EndNumber - data.StartNumber + 1);
                        data.EndNumber = data.StartNumber + data.NumCount - 1;
                        count = count - data.NumCount;
                    }
                    listbox.Add(data);

                }
                foreach (var item in listbox)
                {
                    UserNumBoxContract.Insert(item);
                }

                List<UserNumber> list = new List<UserNumber>();
                foreach (var item in listbox)
                {
                    list.Add(new UserNumber()
                    {
                        UaId = item.UaId,
                        CreatedTime = item.CreatedTime,
                        EndNumber = item.EndNumber,
                        NumCount = item.NumCount,
                        StartNumber = item.StartNumber,
                        UNumBoxId = item.Id,
                        ValueURL = entity.ValueURL,
                        GroupGUID = GroupGUID

                    });
                }
                foreach (var item in list)
                {
                    UserNumberContract.Insert(item);
                }

                if (list.Count > 0)
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "设置成功");
                    return Token(new AjaxResult("设置成功", AjaxResultType.Success, list), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "设置失败");
                    return Token(new AjaxResult("设置失败", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "AutomationUserNumBoxAndNumberInsert", "设置失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }


        #endregion


        #region 获取二维码

        /// <summary>
        /// 根据码获取excel文件数据流
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumberGetCodeExcelData(ParamsGetNuBermExcel entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeExcelData", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeExcelData", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }

            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeExcelData", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                if (UserAllocationContract.Entities.Where(d => d.IsDeleted == false && d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.EndNumber).Count() > 0)
                {
                    var dic = DictionariesContract.Entities;
                    List<ExcelModel> relList = new List<ExcelModel>();
                    //DataTable table = new DataTable();
                    //table.Columns.Add("编号", typeof(string));
                    //table.Columns.Add("内容", typeof(string));
                    for (long i = entity.StartNumber; i <= entity.EndNumber; i++)
                    {
                        relList.Add(new ExcelModel()
                        {
                            Number = i.ToString(),
                            NumberCode = dic.FirstOrDefault(d => d.KeyName == "Url").ValueName + i

                        });
                        // table.Rows.Add(i.ToString(), dic.FirstOrDefault(d => d.KeyName == "Url").ValueName + i);
                    }
                    // string FellPathName = dic.FirstOrDefault(d => d.KeyName == "UplodPath").ValueName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"; ;



                    // var bye = ExcelHelp.AuthGetFileData(table, FellPathName);

                    // string str = System.Text.Encoding.Default.GetString(bye);
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeExcelData", "获取成功");
                    return Token(new AjaxResult("获取成功", AjaxResultType.Success, relList), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeExcelData", "您要获取的活码，不在任何一个分配区间");
                    return Token(new AjaxResult("您要获取的活码，不在任何一个分配区间", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeExcelData", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }


        /// <summary>
        /// 根据码获取二维码图文件数据流
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumberGetCode(ParamsGetNuBerm entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCode", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCode", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }

            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCode", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }

                if (UserAllocationContract.Entities.Where(d => d.IsDeleted == false && d.StartNumber <= entity.Number && d.EndNumber >= entity.Number).Count() > 0)
                {
                    var dic = DictionariesContract.Entities;
                    var url = dic.FirstOrDefault(d => d.KeyName == "Url").ValueName + entity.Number;
                    var Scale = Convert.ToInt32(dic.FirstOrDefault(d => d.KeyName == "Scale").ValueName);
                    var Version = Convert.ToInt32(dic.FirstOrDefault(d => d.KeyName == "Version").ValueName);
                    var XY = Convert.ToInt32(dic.FirstOrDefault(d => d.KeyName == "XY").ValueName);

                    var bye = QRCodeHelp.GetCodBit(url, Scale, Version, XY);

                    //string str = System.Text.Encoding.Default.GetString(bye);
                    InserLog(entity.GuIdNumber, "UserNumberGetCode", "获取成功");
                    return Token(new AjaxResult("获取成功", AjaxResultType.Success, bye), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCode", "您要获取的活码，不在任何一个分配区间");
                    return Token(new AjaxResult("您要获取的活码，不在任何一个分配区间", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCode", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }

        /// <summary>
        /// 根据码获取二维码内容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumberGetCodeValue(ParamsGetNuBerm entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }

            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }
                var data = UserNumberContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false && d.StartNumber <= entity.Number && d.EndNumber >= entity.Number).FirstOrDefault();

                if (data != null)
                {
                    var list = UserNumberContract.Entities.Where(d=> d.IsDeleted == false && d.GroupGUID==data.GroupGUID);

                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "获取成功");
                    return Token(new AjaxResult("获取成功", AjaxResultType.Success, list), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "您要获取的活码部分在");
                    return Token(new AjaxResult("您要获取的活码部分在", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }



        #endregion


        #region 日志
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="apiName"></param>
        /// <param name="Explain"></param>
        private void InserLog(string guid, string apiName, string Explain)
        {
            var udata = "Register,UserAllocation".Contains(apiName) ? null : UserAccountContract.Entities.Where(d => d.GuIdNumber == guid).FirstOrDefault();
            var data = new APILogInfo();
            data.UserGuid = guid;
            data.ApiName = apiName;
            data.Explain = Explain;
            data.UserName = udata == null ? "---" : udata.UserName;

            APILogInfoContract.Insert(data);

        }

        #endregion


        #region 修改填充

        /// <summary>
        /// 根据码修改码所对应的设置区间的整体Url
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumberUpdateCodeValue(ParamsUpdateNuBerm entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }

            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }
                var data = UserNumberContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false && d.StartNumber <= entity.Number && d.EndNumber >= entity.Number).FirstOrDefault();
                if (data != null)
                {
                    var list = UserNumberContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false && d.GroupGUID == data.GroupGUID).ToList();

                    foreach (var item in list)
                    {
                        item.ValueURL = entity.ValueURL;
                        UserNumberContract.Update(item);
                    }
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "获取成功");
                    return Token(new AjaxResult("获取成功", AjaxResultType.Success, list), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "您要获取的活码部分在");
                    return Token(new AjaxResult("您要获取的活码部分在", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }



        /// <summary>
        /// 根据码 将码从设置区间内拆离单独存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumberSplitCodeValue(ParamsSplitNuBerm entity)
        {
            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumberSplitCodeValue", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }

            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumberSplitCodeValue", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }

            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumberSplitCodeValue", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }
                var data = UserNumberContract.Entities.Where(d => d.UaId == dataAccount.Id && d.IsDeleted == false && d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.EndNumber).FirstOrDefault();
                if (data != null)
                {

                    List<UserNumber> list = new List<UserNumber>();

                    ///新填充
                    var newdata = new UserNumber()
                    {
                        UaId = data.UaId,
                        CreatedTime = DateTime.Now,
                        EndNumber = entity.EndNumber,
                        NumCount = Convert.ToInt32(entity.EndNumber + 1 - entity.StartNumber),
                        StartNumber = entity.StartNumber,
                        UNumBoxId = data.UNumBoxId,
                        ValueURL = entity.ValueURL,
                        GroupGUID = Guid.NewGuid().ToString()
                    };
                    UserNumberContract.Insert(newdata);
                    list.Add(newdata);
                    //处理原数据
                    if (data.StartNumber == entity.StartNumber)
                    {
                        data.StartNumber = entity.EndNumber + 1;
                        data.NumCount = Convert.ToInt32(data.NumCount - (entity.EndNumber + 1 - entity.StartNumber));
                        UserNumberContract.Update(data);
                        list.Add(data);
                    }
                    else if (data.EndNumber == entity.EndNumber)
                    {
                        data.EndNumber = entity.StartNumber - 1;
                        data.NumCount = Convert.ToInt32(data.NumCount - (entity.EndNumber + 1 - entity.StartNumber));
                        UserNumberContract.Update(data);
                        list.Add(data);
                    }
                    else
                    {
                        ///形成区间前段信息
                        var startdata = new UserNumber()
                        {
                            UaId = data.UaId,
                            CreatedTime = data.CreatedTime,
                            EndNumber = entity.StartNumber - 1,
                            NumCount = Convert.ToInt32(entity.StartNumber - data.StartNumber),
                            StartNumber = data.StartNumber,
                            UNumBoxId = data.UNumBoxId,
                            ValueURL = data.ValueURL,
                            GroupGUID = data.GroupGUID
                        };
                        UserNumberContract.Insert(startdata);
                        list.Add(startdata);
                        //形成区间后端信息
                        var enddata = new UserNumber()
                        {
                            UaId = data.UaId,
                            CreatedTime = data.CreatedTime,
                            EndNumber = data.EndNumber,
                            NumCount = Convert.ToInt32(data.EndNumber - entity.EndNumber),
                            StartNumber = entity.EndNumber + 1,
                            UNumBoxId = data.UNumBoxId,
                            ValueURL = data.ValueURL,
                            GroupGUID = data.GroupGUID
                        };
                        UserNumberContract.Insert(enddata);
                        list.Add(enddata);
                        //删除老信息
                        UserNumberContract.Delete(UserNumberContract.GetByKey(data.Id));
                    }





                    InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "调整成功");
                    return Token(new AjaxResult("调整成功", AjaxResultType.Success, list), "");
                }
                else
                {
                    InserLog(entity.GuIdNumber, "UserNumberSplitCodeValue", "您要调整活码不在一个固定区域");
                    return Token(new AjaxResult("您要调整活码不在一个固定区域", AjaxResultType.Error), "");
                }


            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumberGetCodeValue", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }


        /// <summary>
        /// 根据箱码标识，重置箱码信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult UserNumBoxReset(ParamsNumBoxList entity)
        {

            if (entity == null)
            {
                InserLog(entity.GuIdNumber, "UserNumBoxReset", "传输参数错误");
                return Token(new AjaxResult("传输参数错误", AjaxResultType.Error), "");
            }


            if (string.IsNullOrWhiteSpace(entity.GuIdNumber))
            {
                InserLog(entity.GuIdNumber, "UserNumBoxReset", "协约唯一标识不能为空");
                return Token(new AjaxResult("协约唯一标识不能为空", AjaxResultType.Error), "");
            }


            try
            {
                var dataAccount = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.GuIdNumber == entity.GuIdNumber).FirstOrDefault();

                if (VerificationUserAccount(dataAccount))
                {
                    InserLog(entity.GuIdNumber, "UserNumBoxReset", "账户没开通或被禁用");
                    return Token(new AjaxResult("账户没开通或被禁用", AjaxResultType.Error), "");
                }
                if (string.IsNullOrWhiteSpace(entity.NumBoxIds) && string.IsNullOrWhiteSpace(entity.Guid))
                {
                    InserLog(entity.GuIdNumber, "UserNumBoxReset", "请输入箱码标识");
                    return Token(new AjaxResult("请输入箱码标识", AjaxResultType.Error), "");
                }
                if (string.IsNullOrWhiteSpace(entity.NumBoxIds))
                {
                    entity.NumBoxIds = "";
                }

                string[] idsStr = entity.NumBoxIds.Split(new char[] { ',' });
                int[] idsInt = entity.NumBoxIds == "" ? null : Array.ConvertAll(idsStr, id => Convert.ToInt32(id));

                var qUserNumBox = entity.NumBoxIds == "" ? UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UaId == dataAccount.Id && d.Guid == entity.Guid) : UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && idsInt.Contains(d.Id));

               var list= qUserNumBox.Select(d => d.Id).ToList();

               UserNumberContract.DeleteDirect(d=> list.Contains(d.UNumBoxId));


                InserLog(entity.GuIdNumber, "UserNumBoxReset", "重置成功");
                return Token(new AjaxResult("重置成功", AjaxResultType.Success), "");



            }
            catch (Exception e)
            {
                InserLog(entity.GuIdNumber, "UserNumBoxReset", "失败");
                return Token(new AjaxResult("失败" + e.Message, AjaxResultType.Error), "");
            }
        }



        #endregion
    }
}
