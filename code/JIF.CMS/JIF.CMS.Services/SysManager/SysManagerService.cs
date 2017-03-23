using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Security;
using System.Security.Cryptography;
using JIF.CMS.Services.SysManager.Dtos;
using System.Linq.Expressions;
using JIF.CMS.Core.Infrastructure;

namespace JIF.CMS.Services.SysManager
{
    public class SysManagerService : BaseService, ISysManagerService
    {
        private readonly IRepository<SysAdmin> _sysAdminRepository;
        private readonly IWorkContext _workContext;

        public SysManagerService(IRepository<SysAdmin> sysAdminRepository,
            IWorkContext workContext)
        {
            _sysAdminRepository = sysAdminRepository;
            _workContext = workContext;
        }

        #region private methods

        /// <summary>
        /// 用户密码加密
        /// </summary>
        /// <param name="pwd">密码明文</param>
        /// <param name="createtime">用户创建时间</param>
        /// <returns></returns>
        private string EncyptPwd(string pwd, DateTime createtime)
        {
            var algo = EncyptHelper.CreateHashAlgoMd5();
            var plain = string.Format("{0}-{1}", pwd, createtime.ToString(JIFConsts.DATETIME_NORMAL));

            return EncyptHelper.Encrypt(algo, plain);
        }

        #endregion

        public SysAdmin Get(int id)
        {
            return _sysAdminRepository.Get(id);
        }

        public void Add(SysAdminInertBasicInfo model)
        {
            if (model == null)
            {
                throwJIFException("信息不能为空");
            }

            if (string.IsNullOrWhiteSpace(model.Account)
                || string.IsNullOrWhiteSpace(model.Password)
                || string.IsNullOrWhiteSpace(model.Email))
            {
                throwJIFException("信息不完整");
            }

            var exists = _sysAdminRepository.Table.Any(d => model.Account.ToLower().Trim() == d.Account.ToLower().Trim());
            if (exists)
            {
                throwJIFException("帐号: " + model.Account + ", 已存在");
            }

            var now = DateTime.Now;

            var entity = new SysAdmin
            {
                Account = model.Account,
                Email = model.Email,
                CellPhone = model.CellPhone,
                Password = EncyptPwd(model.Password, now),
                Enable = model.Enable,
                CreateTime = now,
                CreateUserId = _workContext.CurrentUser.Id
            };

            _sysAdminRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBasicInfo(int id, SysAdminUpdateBasicInfo model)
        {
            if (model == null)
            {
                throwJIFException("信息不能为空");
            }

            var entity = _sysAdminRepository.Get(id);
            if (entity == null)
            {
                throwJIFException("用户不存在");
            }

            entity.Email = model.Email;
            entity.CellPhone = model.CellPhone;
            entity.Enable = model.Enable;

            _sysAdminRepository.Update(entity);

        }

        public void UpdatePwd(int id, string newPwd)
        {
            if (string.IsNullOrWhiteSpace(newPwd))
            {
                throwJIFException("密码不能为空");
            }

            var entity = _sysAdminRepository.Get(id);
            if (entity == null)
            {
                throwJIFException("用户不存在");
            }

            entity.Password = EncyptPwd(newPwd, entity.CreateTime);

            _sysAdminRepository.Update(entity);
        }

        public IPagedList<SysAdminSearchListOutDto> Load(string q, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = (from a in _sysAdminRepository.Table
                         join b in _sysAdminRepository.Table
                         on a.CreateUserId equals b.Id
                         where a.Account.Contains(q) || a.Email.Contains(q) || a.CellPhone.Contains(q)
                         select new SysAdminSearchListOutDto
                         {
                             Id = a.Id,
                             Account = a.Account,
                             Email = a.Email,
                             CellPhone = a.CellPhone,
                             CreateTime = a.CreateTime,
                             CreateUserName = b.Account,
                             Enable = a.Enable
                         }).OrderByDescending(d => d.Id);

            return new PagedList<SysAdminSearchListOutDto>(query, pageIndex, pageSize);
        }

        public LoginOutputDto Login(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
            {
                throwJIFException("账号 / 密码 不能为空");
            }

            var entity = _sysAdminRepository.Table.FirstOrDefault(d => d.Account.ToLower().Trim() == account.ToLower().Trim());

            if (entity == null)
                throw new JIFException("账号不存在");

            var cipherText = EncyptPwd(password, entity.CreateTime);

            if (cipherText != entity.Password)
            {
                throwJIFException("密码不正确");
            }

            //entity.LastLoginTime = DateTime.Now;
            //entity.LastLoginIP = _webHelper.GetCurrentIpAddress();

            //_userRepository.Update(entity);

            return new LoginOutputDto
            {
                UserId = entity.Id
            };
        }
    }
}
