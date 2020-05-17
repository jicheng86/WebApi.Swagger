using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Swagger.DomainModel;

namespace WebApi.Swagger.Service
{
    public interface IJwtAuthenticateService
    {
        /// <summary>
        /// 当前访问是否已授权jwt
        /// </summary>
        /// <param name="request"></param>
        /// <param name="jwtString"></param>
        /// <returns></returns>
        bool IsAuthenticated(RequestDTO request, out string jwtString);
    }
}
