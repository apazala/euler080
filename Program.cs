using System.Numerics;

class ACoeff
{
    private int a0;

    public int A0 => a0;
    List<int> aPeriod;

    public ACoeff(int n)
    {
        aPeriod = new List<int>();
        a0 = (int)Math.Sqrt(n);
        int a, b, c, b0, c0;
        c = n - a0 * a0;
        if (c == 0) return;

        c0 = c;
        b = b0 = a0;
        int a02 = (a0 +a0);
        do
        {
            a = (a0 + b) / c;
            b = a * c - b;
            c = (n - b * b) / c;
            aPeriod.Add(a);
        } while (a != a02);
    }

    public int GetAk(int k){
        if (k<= aPeriod.Count) return (k == 0)?a0:aPeriod[k-1];

        return aPeriod[(k - 1) % aPeriod.Count];
    }

    public bool IsPerfectSquare{
        get{return aPeriod.Count == 0;}
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        BigInteger mult = BigInteger.Pow(10, 100);
        int sum = 0;
        for(int n = 2; n <= 100; n++){
            ACoeff aCoeff = new ACoeff(n);
            if(aCoeff.IsPerfectSquare) continue;

            BigInteger an2, an1 = 0, a = 1, bn2, bn1 = 1, b = 0;

            for(int k = 0; 2*b*b/aCoeff.A0 < mult; k++){
                an2 = an1; an1 = a; bn2 = bn1; bn1 = b;
                int ak = aCoeff.GetAk(k);
                a = ak*an1 + an2;
                b = ak*bn1 + bn2;
            }
            string c = $"{a*mult/b}";
            for(int i = 0; i < 100; i++){
                sum += c[i]-'0';
            }
        }
        Console.WriteLine(sum);
    }
}