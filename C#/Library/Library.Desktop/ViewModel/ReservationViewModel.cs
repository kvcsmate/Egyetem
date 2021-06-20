using Library.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Desktop.ViewModel
{
    public class ReservationViewModel:ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private DateTime _start;
        public DateTime Start
        {
            get { return _start; }
            set { _start = value; OnPropertyChanged(); }
        }

        private DateTime _end;
        public DateTime End
        {
            get { return _end; }
            set { _end = value; OnPropertyChanged(); }
        }

        private int _volumeId;
        public int VolumeId
        {
            get { return _volumeId; }
            set { _volumeId = value; OnPropertyChanged(); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(); }
        }
        public ReservationViewModel ShallowClone()
        {
            return (ReservationViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ReservationViewModel v)
        {
            IsActive = v.IsActive;
            Id = v.Id;
            Start = v.Start;
            End = v.End;
            VolumeId = v.VolumeId;
        }

        public static explicit operator ReservationViewModel(ReservationDto dto) => new ReservationViewModel
        {
            IsActive = dto.IsActive,
            Id = dto.Id,
            Start = dto.Start,
            End = dto.End,
            VolumeId = dto.VolumeId

        };

        public static explicit operator ReservationDto(ReservationViewModel v) => new ReservationDto
        {
            IsActive = v.IsActive,
            Id = v.Id,
            Start = v.Start,
            End = v.End,
            VolumeId = v.VolumeId
        };
    }
}
