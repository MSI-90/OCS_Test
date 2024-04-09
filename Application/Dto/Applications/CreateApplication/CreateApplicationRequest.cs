﻿using NotionTestWork.Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Applications.CreateApplication;

public class CreateApplicationRequest
{
    [Required(ErrorMessage = "Обязательно к заполнению")]
    public Guid Author { get; set; }
    public ActivityEnum Activity { get; set; } = ActivityEnum.Report;

    [Required(ErrorMessage = "Обязательно к заполнению")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Обязательно к заполнению")]
    [MaxLength(1000)]
    public string Outline { get; set; } = string.Empty;
}
