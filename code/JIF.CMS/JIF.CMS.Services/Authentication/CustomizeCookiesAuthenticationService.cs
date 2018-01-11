using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Services.Authentication
{
    public class CustomizeCookiesAuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<SysAdmin> _sysAdminRepository;

        public CustomizeCookiesAuthenticationService(
            ICacheManager cacheManager,
            IRepository<SysAdmin> sysAdminRepository)
        {
            _cacheManager = cacheManager;
            _sysAdminRepository = sysAdminRepository;
        }

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

        public void SignIn(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
                throw new JIFException("账号 / 密码 不能为空");

            var entity = _sysAdminRepository.Table.FirstOrDefault(d => d.Account.ToLower().Trim() == account.ToLower().Trim());

            if (entity == null)
                throw new JIFException("账号不存在");

            if (!entity.Enable)
                throw new JIFException("账号已被停用");

            var cipherText = EncyptPwd(password, entity.CreateTime);

            if (cipherText != entity.Password)
                throw new JIFException("密码不正确");

            // TODO: 连续错误密码三次, 缓存记录. 限制 10min. 
            CacheKeyConstants.LOGIN_PASSWORD_ERROR_COUNT.ToString();

            // TODO: 记录登陆日志

            // TODO: 登陆成功, 存入缓存, 写入cookies

        }

        public void SignOut()
        {
            // TODO: 清除缓存, cookie;

            // TODO: 记录登出日志

            throw new NotImplementedException();
        }
    }
}
