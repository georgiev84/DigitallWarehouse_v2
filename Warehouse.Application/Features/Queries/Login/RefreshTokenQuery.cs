using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Login;
public record RefreshTokenQuery(string Token, string RefreshToken) : IRequest<RefreshModel>;
