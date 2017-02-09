using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainModels
{
    public class TCarInfo
    {
        private int carID;
        private string carBrand;
        private string carName;
        private string carNumber;
        private int carTrevalDistance;
        private DateTime carComeTime;
        private DateTime carLeaveTime;
        private string carOwner;
        private string carOwnerTel1;
        private string carOwnerTel2;
        private string carOwnerTel3;
        private string carOwnerAddress;
        private string carPhotos;
        private string oprtUserCode;

        public int CarID
        {
            get
            {
                return carID;
            }

            set
            {
                carID = value;
            }
        }

        public string CarBrand
        {
            get
            {
                return carBrand;
            }

            set
            {
                carBrand = value;
            }
        }

        public string CarName
        {
            get
            {
                return carName;
            }

            set
            {
                carName = value;
            }
        }

        public string CarNumber
        {
            get
            {
                return carNumber;
            }

            set
            {
                carNumber = value;
            }
        }

        public int CarTrevalDistance
        {
            get
            {
                return carTrevalDistance;
            }

            set
            {
                carTrevalDistance = value;
            }
        }

        public DateTime CarComeTime
        {
            get
            {
                return carComeTime;
            }

            set
            {
                carComeTime = value;
            }
        }

        public DateTime CarLeaveTime
        {
            get
            {
                return carLeaveTime;
            }

            set
            {
                carLeaveTime = value;
            }
        }

        public string CarOwner
        {
            get
            {
                return carOwner;
            }

            set
            {
                carOwner = value;
            }
        }

        public string CarOwnerTel1
        {
            get
            {
                return carOwnerTel1;
            }

            set
            {
                carOwnerTel1 = value;
            }
        }

        public string CarOwnerTel2
        {
            get
            {
                return carOwnerTel2;
            }

            set
            {
                carOwnerTel2 = value;
            }
        }

        public string CarOwnerTel3
        {
            get
            {
                return carOwnerTel3;
            }

            set
            {
                carOwnerTel3 = value;
            }
        }

        public string CarOwnerAddress
        {
            get
            {
                return carOwnerAddress;
            }

            set
            {
                carOwnerAddress = value;
            }
        }

        public string CarPhotos
        {
            get
            {
                return carPhotos;
            }

            set
            {
                carPhotos = value;
            }
        }

        public string OprtUserCode
        {
            get
            {
                return oprtUserCode;
            }

            set
            {
                oprtUserCode = value;
            }
        }
    }
}
