﻿// <autogenerated>
//   This file was generated by T4 code generator ConfigurationCodeScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

// -----------------------------------------------------------------------
//  <copyright file="UserAccountConfiguration.Generated.cs">
//      Copyright (c) 2017 TomNet. All rights reserved.
//  </copyright>
//  <last-editor>TomNet</last-editor>
// -----------------------------------------------------------------------

using System;
using TomNet.Data.Entity;
using Source.Model.DbModels.QRCode;


namespace Source.Core.ModelConfigurations.QRCode
{
    /// <summary>
    /// 实体类-数据表映射 -- Source.Model.DbModels.QRCode.UserAccount
    /// </summary> 
	public partial class UserAccountConfiguration : EntityConfigurationBase<UserAccount, int>
    { 
        /// <summary>
        /// 初始化一个<see cref="UserAccountConfiguration"/>类型的新实例
        /// </summary>
        public UserAccountConfiguration()
        {
            UserAccountConfigurationAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void UserAccountConfigurationAppend();
    }
}

