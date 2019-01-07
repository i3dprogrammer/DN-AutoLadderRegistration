using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLadderRegistration
{
    class Opcodes
    {
        public const ushort INVENTORY_ITEMS = 0x0701;
        public const ushort STORAGE_ITEMS = 0x70E;
        public const ushort FRIEND_LIST = 0x0F01;

        public const ushort SOCK_CONNECTED = 0x0217;

        public const ushort REQUEST_LOGIN = 0x0128;
        public const ushort REQUEST_VERSION_CHECK = 0x0101;
        public const ushort LOGIN_RESPONSE = 0x0102;
        public const ushort REQUEST_SERVERS = 0x0103;
        public const ushort REQUEST_CHARACTER_LIST = 0x0104;
        public const ushort CHARACTER_LIST_RESPONSE = 0x0104;
        public const ushort REQUSET_CHANNELS = 0x0105;
        public const ushort CHANNELS_RESPONSE = 0x0105;

        public const ushort SERVER_020E_CHECK = 0x020E;
        public const ushort SERVER_BASE64_CHECK_18 = 0x0218;
        public const ushort SERVER_BASE64_CHECK_19 = 0x0219;
        public const ushort ANSWER_BASE64_CHECK_18 = 0x0212;
        public const ushort ANSWER_BASE64_CHECK_19 = 0x0213;
        public const ushort SERVER_0202_ECHO = 0x0202;
        public const ushort SERVER_0301_ECHO = 0x0301;
    }
}
