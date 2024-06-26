﻿using NotionTestWork.Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Applications.UpdateApplication;

public class UpdateApplicationRequest
{
    public ActivityEnum Activity { get; set; } = ActivityEnum.Report;

    [MaxLength(100)]
    public string? Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Outline { get; set; } = string.Empty;
}
