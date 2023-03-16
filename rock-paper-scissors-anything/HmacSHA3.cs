using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace rock_paper_scissors_anything
{
    internal class HmacSHA3
    {
        private readonly HMac hmac;

        /// <summary>
        /// Constructor that creates a hmac object based on Sha3 encryption.
        /// </summary>
        public HmacSHA3(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            hmac = new HMac(new Sha3Digest());
            hmac.Init(new KeyParameter(Convert.FromHexString(key)));
        }

        /// <summary>
        /// Returns HMAC of a given message (text) generated based on the provided key.
        /// </summary>
        /// <returns>HMAC of a text based on key</returns>
        /// <param name="text"> Message that needs to be HMAC'ed</param>
        public string GetHMAC(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException();
            }
            Byte[] text_to_hmac = Encoding.UTF8.GetBytes(text);
            Byte[] output = new byte[hmac.GetMacSize()];
            hmac.BlockUpdate(text_to_hmac);
            hmac.DoFinal(output, 0);
            return Convert.ToHexString(output);
        }
    }
}
