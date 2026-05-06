using System;

class ProyectoMetodos
{
    static double f(double x, double y) => 2 * x - 3 * y + 1;

    static double ObtenerValorReal(double x) 
    {
        return (1.0 / 9.0) + (2.0 / 3.0) * x + (38.0 / 9.0) * Math.Exp(-3 * (x - 1));
    }

    static void Main()
    {
        EjecutarTarea1(0.1);

        EjecutarTarea1(0.05);

        EjecutarTarea2(0.1);

        EjecutarTarea3(0.1);

    }

    static void EjecutarTarea1(double h)

    {
        double x = 1.0;

        double yEuler = 5.0;

        double yEulerMej = 5.0;
        
        double xFinal = 1.5;


     //textos
        Console.WriteLine("\n--- TAREA 1: REPORTE DE ERRORES (h = " + h + ") ---");

        Console.WriteLine("xn\ty_Euler\ty_Mejorado\tValorReal\tErrorAbs\t%ErrorRel");

        while (x <= xFinal + (h / 10))
        {
            double vReal = ObtenerValorReal(x);
            
            // Cálculo automático de errores (basado en Euler Mejorado)
            double errAbs = Math.Abs(vReal - yEulerMej);
            double errRel = (vReal != 0) ? (errAbs / Math.Abs(vReal)) * 100 : 0;

            // Imprimimos todo de una vez
            Console.WriteLine(x.ToString("F2") + "\t" + 
                              yEuler.ToString("F4") + "\t" + 
                              yEulerMej.ToString("F4") + "\t" + 
                              vReal.ToString("F4") + "\t" + 
                              errAbs.ToString("F4") + "\t" + 
                              errRel.ToString("F2") + "%");

            // Método de Euler[cite: 1]
            yEuler += h * f(x, yEuler);

            // Método de Euler Mejorado[cite: 1]
            double k1 = f(x, yEulerMej);
            double k2 = f(x + h, yEulerMej + h * k1);
            yEulerMej += (h / 2.0) * (k1 + k2);

            x += h;
        }
    }
    
    static void EjecutarTarea2(double h)
    {
    double x = 1.0;
    double yRK4 = 5.0;
    double xFinal = 1.5;

    Console.WriteLine("\n--- TAREA 2: REPORTE RK4 (h = " + h + ") ---");
    Console.WriteLine("xn\ty_RK4\tValorReal\tErrorAbs\t%ErrorRel");

        while (x <= xFinal + (h / 10))
        {
          double vReal = ObtenerValorReal(x);
          double errAbs = Math.Abs(vReal - yRK4);
          double errRel = (vReal != 0) ? (errAbs / Math.Abs(vReal)) * 100 : 0;

          Console.WriteLine($"{x:F2}\t{yRK4:F4}\t{vReal:F4}\t{errAbs:F4}\t{errRel:F2}%");

          // Método RK4
          double k1 = f(x, yRK4);
          double k2 = f(x + h / 2.0, yRK4 + (h / 2.0) * k1);
          double k3 = f(x + h / 2.0, yRK4 + (h / 2.0) * k2);
          double k4 = f(x + h, yRK4 + h * k3);

          yRK4 += (h / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
          x += h;
         }
    }
    static void EjecutarTarea3(double h)
{
    // Cambiamos 'x' y 'y' por 'vx' y 'vy' para que coincidan con el resto del código
    double[] vx = new double[5];
    double[] vy = new double[5];

    vx[0] = 0;
    vy[0] = 2; // Dato inicial y(0)=2

    Console.WriteLine("\n--- punto 3 ---");
    Console.WriteLine("xn\tyn");
    Console.WriteLine(vx[0].ToString("F1") + "\t" + vy[0].ToString("F4"));

    // PASO 1: Usar RK4 para obtener y1, y2 y y3[cite: 1]
    for (int i = 0; i < 3; i++)
    {
        // Usamos nombres temporales (xi, yi) para no chocar con los arreglos
        double xi = vx[i];
        double yi = vy[i];

        double k1 = 4 * xi - 2 * yi;
        double k2 = 4 * (xi + h / 2) - 2 * (yi + (h / 2) * k1);
        double k3 = 4 * (xi + h / 2) - 2 * (yi + (h / 2) * k2);
        double k4 = 4 * (xi + h) - 2 * (yi + h * k3);

        vy[i + 1] = yi + (h / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
        vx[i + 1] = xi + h;

        Console.WriteLine(vx[i + 1].ToString("F1") + "\t" + vy[i + 1].ToString("F4"));
    }

        // Paso final y(0.4) usando Adams-Bashforth-Moulton[cite: 1]
        double f3 = 4 * vx[3] - 2 * vy[3];
        double f2 = 4 * vx[2] - 2 * vy[2];
        double f1 = 4 * vx[1] - 2 * vy[1];
        double f0 = 4 * vx[0] - 2 * vy[0];

        // Predictor
        double yp = vy[3] + (h / 24.0) * (55 * f3 - 59 * f2 + 37 * f1 - 9 * f0);

        // Correccion
        double x_sig = vx[3] + h;
        double f_sig = 4 * x_sig - 2 * yp; 

        double yc = vy[3] + (h / 24.0) * (9 * f_sig + 19 * f3 - 5 * f2 + f1);

        Console.WriteLine(x_sig.ToString("F1") + "\t" + yc.ToString("F4"));
    }
}
