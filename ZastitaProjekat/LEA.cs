
using System;
using System.Security.Cryptography;
using System.Text;


public class LEACipher
{
    private static uint[] delta = {
        0xc3efe9db,
        0x44626b02,
        0x79e27c8a,
        0x78df30ec,
        0x715ea49e,
        0xc785da0a,
        0xe04ef22a,
        0xe5c40957
    };

    private static uint ROL(int i, uint value)
    {
        return (value << i) | (value >> (32 - i));
    }

    public static uint ROR(int i, uint value)
    {
        return (value >> i) | (value << (32 - i));
    }

    public static void KeySchedule_128(byte[] K, uint[] RK)
    {
        uint[] T = new uint[4];
        Buffer.BlockCopy(K, 0, T, 0, 16);

        for (int i = 0; i < 24; i++)
        {
            T[0] = ROL(1, T[0] + ROL(i, delta[i % 4]));
            T[1] = ROL(3, T[1] + ROL(i + 1, delta[i % 4]));
            T[2] = ROL(6, T[2] + ROL(i + 2, delta[i % 4]));
            T[3] = ROL(11, T[3] + ROL(i + 3, delta[i % 4]));
            RK[i * 6 + 0] = T[0];
            RK[i * 6 + 1] = T[1];
            RK[i * 6 + 2] = T[2];
            RK[i * 6 + 3] = T[1];
            RK[i * 6 + 4] = T[3];
            RK[i * 6 + 5] = T[1];
        }
    }

    public static void KeySchedule_192(byte[] K, uint[] RK)
    {
        uint[] T = new uint[6];
        Buffer.BlockCopy(K, 0, T, 0, 24);

        for (int i = 0; i < 28; i++)
        {
            T[0] = ROL(1, T[0] + ROL(i, delta[i % 6]));
            T[1] = ROL(3, T[1] + ROL(i + 1, delta[i % 6]));
            T[2] = ROL(6, T[2] + ROL(i + 2, delta[i % 6]));
            T[3] = ROL(11, T[3] + ROL(i + 3, delta[i % 6]));
            T[4] = ROL(13, T[4] + ROL(i + 4, delta[i % 6]));
            T[5] = ROL(17, T[5] + ROL(i + 5, delta[i % 6]));
            RK[i * 6 + 0] = T[0];
            RK[i * 6 + 1] = T[1];
            RK[i * 6 + 2] = T[2];
            RK[i * 6 + 3] = T[3];
            RK[i * 6 + 4] = T[4];
            RK[i * 6 + 5] = T[5];
        }
    }

    public static void KeySchedule_256(byte[] K, uint[] RK)
    {
        uint[] T = new uint[8];
        Buffer.BlockCopy(K, 0, T, 0, 32);

        for (int i = 0; i < 32; i++)
        {
            T[(6 * i + 0) % 8] = ROL(1, T[(6 * i + 0) % 8] + ROL(i, delta[i % 8]));
            T[(6 * i + 1) % 8] = ROL(3, T[(6 * i + 1) % 8] + ROL(i + 1, delta[i % 8]));
            T[(6 * i + 2) % 8] = ROL(6, T[(6 * i + 2) % 8] + ROL(i + 2, delta[i % 8]));
            T[(6 * i + 3) % 8] = ROL(11, T[(6 * i + 3) % 8] + ROL(i + 3, delta[i % 8]));
            T[(6 * i + 4) % 8] = ROL(13, T[(6 * i + 4) % 8] + ROL(i + 4, delta[i % 8]));
            T[(6 * i + 5) % 8] = ROL(17, T[(6 * i + 5) % 8] + ROL(i + 5, delta[i % 8]));
            RK[i * 6 + 0] = T[(i * 6 + 0) % 8];
            RK[i * 6 + 1] = T[(i * 6 + 1) % 8];
            RK[i * 6 + 2] = T[(i * 6 + 2) % 8];
            RK[i * 6 + 3] = T[(i * 6 + 3) % 8];
            RK[i * 6 + 4] = T[(i * 6 + 4) % 8];
            RK[i * 6 + 5] = T[(i * 6 + 5) % 8];
        }
    }

    public static void Encrypt(int Nr, uint[] RK, byte[] P, byte[] C)
    {
        uint[] X_Round = new uint[4];
        uint[] X_NextRound = new uint[4];
        Buffer.BlockCopy(P, 0, X_Round, 0, 16);

        for (int i = 0; i < Nr; i++)
        {
            X_NextRound[0] = ROL(9, (X_Round[0] ^ RK[i * 6 + 0]) + (X_Round[1] ^ RK[i * 6 + 1]));
            X_NextRound[1] = ROR(5, (X_Round[1] ^ RK[i * 6 + 2]) + (X_Round[2] ^ RK[i * 6 + 3]));
            X_NextRound[2] = ROR(3, (X_Round[2] ^ RK[i * 6 + 4]) + (X_Round[3] ^ RK[i * 6 + 5]));
            X_NextRound[3] = X_Round[0];

            Buffer.BlockCopy(X_NextRound, 0, X_Round, 0, 16);
        }

        Buffer.BlockCopy(X_NextRound, 0, C, 0, 16);
    }


    public static void Decrypt(int Nr, uint[] RK, byte[] D, byte[] C)
    {
        uint[] X_Round = new uint[4];
        uint[] X_NextRound = new uint[4];
        Buffer.BlockCopy(C, 0, X_Round, 0, 16);

        for (int i = 0; i < Nr; i++)
        {
            X_NextRound[0] = X_Round[3];
            X_NextRound[1] = (ROR(9, X_Round[0]) - (X_NextRound[0] ^ RK[((Nr - i - 1) * 6) + 0])) ^ RK[((Nr - i - 1) * 6) + 1];
            X_NextRound[2] = (ROL(5, X_Round[1]) - (X_NextRound[1] ^ RK[((Nr - i - 1) * 6) + 2])) ^ RK[((Nr - i - 1) * 6) + 3];
            X_NextRound[3] = (ROL(3, X_Round[2]) - (X_NextRound[2] ^ RK[((Nr - i - 1) * 6) + 4])) ^ RK[((Nr - i - 1) * 6) + 5];

            Buffer.BlockCopy(X_NextRound, 0, X_Round, 0, 16);
        }

        Buffer.BlockCopy(X_Round, 0, D, 0, 16);
    }




    public static void EncryptCTR(int Nr, uint[] RK, byte[] IV, byte[] P, byte[] C)
    {
        int blockCount = P.Length / 16;
        byte[] counter = new byte[16];

        for (int i = 0; i < blockCount; i++)
        {
            GenerateCounter(counter, BitConverter.ToUInt64(IV, 0) + (ulong)i);
            byte[] encryptedCounter = new byte[16];
            Encrypt(Nr, RK, counter, encryptedCounter);

            for (int j = 0; j < 16; j++)
            {
                C[i * 16 + j] = (byte)(P[i * 16 + j] ^ encryptedCounter[j]);
            }
        }
    }

    public static void DecryptCTR(int Nr, uint[] RK, byte[] IV, byte[] D, byte[] C)
    {
        int blockCount = C.Length / 16;
        byte[] counter = new byte[16];

        for (int i = 0; i < blockCount; i++)
        {
            GenerateCounter(counter, BitConverter.ToUInt64(IV, 0) + (ulong)i);
            byte[] encryptedCounter = new byte[16];

            Encrypt(Nr, RK, counter, encryptedCounter);

            for (int j = 0; j < 16; j++)
            {
                D[i * 16 + j] = (byte)(C[i * 16 + j] ^ encryptedCounter[j]);
            }
        }
    }

   

    private static void GenerateCounter(byte[] counter, ulong counterValue)
    {
        byte[] valueBytes = BitConverter.GetBytes(counterValue);

        for (int i = 0; i < 8; i++)
        {
            counter[i] = valueBytes[i];
        }

        for (int i = 8; i < 16; i++)
        {
            counter[i] = 0;
        }
    }




}