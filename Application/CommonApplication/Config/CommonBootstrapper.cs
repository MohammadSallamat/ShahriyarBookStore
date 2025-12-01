using Application.CommonApplication.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommonApplication.Config;


public class CommonBootstrapper
{
    public static void Init(IServiceCollection service)
    {
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
    }
}