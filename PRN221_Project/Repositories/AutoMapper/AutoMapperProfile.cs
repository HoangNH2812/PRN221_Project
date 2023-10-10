using AutoMapper;
using Repositories.DataTranferObject;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppointmentDetailDTO, AppointmentDetail>();
            CreateMap<AppointmentDetail, AppointmentDetailDTO>();

            CreateMap<AppointmentDTO, Appointment>();
            CreateMap<Appointment, AppointmentDTO>();

            CreateMap<Artist, ArtistDTO>();
            CreateMap<Artist, ArtistDTO>();

            CreateMap<CertificateArtistDTO, CertificateArtist>();
            CreateMap<CertificateArtist, CertificateArtistDTO>();

            CreateMap<CertificateDTO, Certificate>();
            CreateMap<Certificate, CertificateDTO>();

            CreateMap<LoginDTO, Login>();
            CreateMap<Login, LoginDTO>();

            CreateMap<ScheduleDTO, Schedule>();
            CreateMap<Schedule, ScheduleDTO>();

            CreateMap<ServiceDTO, Service>();
            CreateMap<Service, ServiceDTO>();

            CreateMap<StaffDTO, Staff>();
            CreateMap<Staff, StaffDTO>();

            CreateMap<StudioDTO, Studio>();
            CreateMap<Studio, StudioDTO>();

            CreateMap<StyleDTO, Style>();
            CreateMap<Style, StyleDTO>();

            CreateMap<TattoosDesignDTO, TattoosDesign>();
            CreateMap<TattoosDesign, TattoosDesignDTO>();

            CreateMap<TattooLoverDTO, TattooLover>();
            CreateMap<TattooLover, TattooLoverDTO>();


        }
    }
}
