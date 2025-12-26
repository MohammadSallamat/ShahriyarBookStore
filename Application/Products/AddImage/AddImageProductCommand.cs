using Application.CommonApplication;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.AddImage;

public record AddImageProductCommand(long ProductId, IFormFile ImageName, int Sequence):IBaseCommand;
