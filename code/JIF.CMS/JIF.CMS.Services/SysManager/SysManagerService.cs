using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Helpers;
using JIF.CMS.Services.SysManager.Dtos;
using System;
using System.Linq;

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
            var algo = EncryptHelper.CreateHashAlgoMd5();
            var plain = string.Format("{0}-{1}", pwd, createtime.ToString(JIFConstants.DATETIME_NORMAL));

            return EncryptHelper.Encrypt(algo, plain);
        }

        #endregion

        public SysAdmin Get(int id)
        {
            return _sysAdminRepository.Get(id);
        }

        public void Add(SysAdminInertBasicInfoInput model)
        {
            if (model == null)
                throw new JIFException("信息不能为空");

            if (string.IsNullOrWhiteSpace(model.Account)
                || string.IsNullOrWhiteSpace(model.Password)
                || string.IsNullOrWhiteSpace(model.Email))
            {
                throw new JIFException("信息不完整");
            }

            var exists = _sysAdminRepository.Table.Any(d => model.Account.ToLower().Trim() == d.Account.ToLower().Trim());
            if (exists)
                throw new JIFException("帐号: " + model.Account + ", 已存在");

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

        public void UpdateBasicInfo(int id, SysAdminUpdateBasicInfoInput model)
        {
            if (model == null)
                throw new JIFException("信息不能为空");

            var entity = _sysAdminRepository.Get(id);
            if (entity == null)
                throw new JIFException("用户不存在");

            entity.Email = model.Email;
            entity.CellPhone = model.CellPhone;
            entity.Enable = model.Enable;

            _sysAdminRepository.Update(entity);

        }

        public void UpdatePwd(int id, string newPwd)
        {
            if (string.IsNullOrWhiteSpace(newPwd))
                throw new JIFException("密码不能为空");

            var entity = _sysAdminRepository.Get(id);
            if (entity == null)
                throw new JIFException("用户不存在");

            entity.Password = EncyptPwd(newPwd, entity.CreateTime);

            _sysAdminRepository.Update(entity);
        }

        public IPagedList<SysAdminSearchListOutput> Get(string q, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = (from a in _sysAdminRepository.Table
                         join b in _sysAdminRepository.Table
                         on a.CreateUserId equals b.Id
                         where a.Account.Contains(q) || a.Email.Contains(q) || a.CellPhone.Contains(q)
                         select new SysAdminSearchListOutput
                         {
                             Id = a.Id,
                             Account = a.Account,
                             Email = a.Email,
                             CellPhone = a.CellPhone,
                             CreateTime = a.CreateTime,
                             CreateUserName = b.Account,
                             Enable = a.Enable
                         }).OrderByDescending(d => d.Id);

            return new PagedList<SysAdminSearchListOutput>(query, pageIndex, pageSize);
        }
    }
}
