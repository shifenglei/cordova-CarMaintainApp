using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainModels
{
    public class TUserInfo
    {
        private int uid;

        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }

        public string Ucode
        {
            get
            {
                return ucode;
            }

            set
            {
                ucode = value;
            }
        }

        public string Uname
        {
            get
            {
                return uname;
            }

            set
            {
                uname = value;
            }
        }

        public string Upassword
        {
            get
            {
                return upassword;
            }

            set
            {
                upassword = value;
            }
        }


        public int Ustate
        {
            get
            {
                return ustate;
            }

            set
            {
                ustate = value;
            }
        }

        public DateTime? Uentrytime
        {
            get
            {
                return uentrytime;
            }

            set
            {
                uentrytime = value;
            }
        }

        public string Uremark
        {
            get
            {
                return uremark;
            }

            set
            {
                uremark = value;
            }
        }

        public string Uphoto
        {
            get
            {
                return uphoto;
            }

            set
            {
                uphoto = value;
            }
        }

        public string Ubrithday
        {
            get
            {
                return ubrithday;
            }

            set
            {
                ubrithday = value;
            }
        }

        public string Uaddress
        {
            get
            {
                return uaddress;
            }

            set
            {
                uaddress = value;
            }
        }

        public string Utel2
        {
            get
            {
                return utel2;
            }

            set
            {
                utel2 = value;
            }
        }

        public string Utel1
        {
            get
            {
                return utel1;
            }

            set
            {
                utel1 = value;
            }
        }

        public string Uidentitycardno
        {
            get
            {
                return uidentitycardno;
            }

            set
            {
                uidentitycardno = value;
            }
        }

        public List<TGroupInfo> Usergroups
        {
            get
            {
                return usergroups;
            }

            set
            {
                usergroups = value;
            }
        }

        private string ucode;

        private string uname;

        private string upassword;

        private string uidentitycardno;

        private string utel1;

        private string utel2;

        private string uaddress;

        private string ubrithday;

        private string uphoto;

        private string uremark;

        private DateTime? uentrytime;

        private int ustate;

        private List<TGroupInfo> usergroups;
    }
}
