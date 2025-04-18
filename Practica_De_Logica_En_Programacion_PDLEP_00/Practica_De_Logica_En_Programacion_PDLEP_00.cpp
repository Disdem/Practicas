// Practica_De_Logica_En_Programacion_PDLEP_00.cpp : Este archivo contiene la función "main". La ejecución del programa comienza y termina ahí.
//

#include <iostream>

/*
* Cuando se crea una funcion es importante el orded, ya que la computadora o progama lee el codigo de arriba hacia abajo,
* por eso es importante que la funcion sea declarada antes de ser llamada o en su defecto tener una logica de orden y continuacion
*/

// Reto extra, hacer una funcion para verificar si el numero es palindromo o no y llamarla desde el main
static void palindromo() {
	int numero = 0; // Variable para almacenar el numero ingresado por el usuario

	// Solicitamos el numero al usuario
	std::cout << "Ingrese un numero entero positivo: ";
	std::cin >> numero;

	/*
	* Complemento de codigo 2.
	* verificar que el numero ingresado sea positivo, para esto usaremos un siclo while para comprobar que el numero es positivo o en otras
	* palabras, que el numero sea mayor a cero
	*/

	// Ciclo para verificar que el numero ingresado es positivo
	while (numero <= 0)
	{
		std::cout << "Lo siento, pero el numero ingresado debe ser positivo o mayor a 0, ingresa de nuevo tu numero" << std::endl;
		std::cin >> numero; // Volvemos a solicitar el numero al usuario
	}

	// Guardamos el numero ingresado en una variable auxiliar
	int numOriginal = numero;

	// Creamos una variable para almacenar el numero invertido
	int numInvertido = 0;

	// Mostramos el numero ingresado al usuario para asegurar que se ingreso correctamente
	std::cout << "El numero ingresado es: " << numero << std::endl;

	// Invertimos el numero con un siclo while
	// usando el operador modulo para obtener el digito menos significativo

	while (numero > 0) {
		// Obtenemos el digito menos significativo
		int digito = numero % 10;
		// Agregamos el digito al numero invertido
		numInvertido = numInvertido * 10 + digito;
		// Eliminamos el digito menos significativo del numero original
		numero /= 10;
	}

	// Mostramos el numero invertido al usuario
	// Comparando si es un palindromo o no

	if (numOriginal == numInvertido) {
		std::cout << "El numero ingresado: " << numOriginal << " es un palindromo!" << std::endl;
	}
	else {
		std::cout << "El numero ingresado: " << numOriginal << " no es un palindromo!" << std::endl;
	}
}

int main()
{
	/*Reto de programacion 1.
	Número palíndromo
	Enunciado: Dado un número entero positivo,
	determina si es un número palíndromo (es decir, si se lee igual de izquierda a derecha que de derecha a izquierda).
	*/

	/*
	* Complemento de codigo 1
	* lograr que todo el codigo pueda ser repetido o ser cancelado con la respuesta del usuario
	*/

	//Variables a usar
	char respuesta = 's'; // Variable para almacenar la respuesta del usuario al repetir el programa

	// Ciclo para repetir el programa a solicitud del usuario

	do
	{
		// Llamamos a la funcion palindromo
		palindromo();

		// Preguntamos al usuario si desea repetir el programa
		std::cout << "¿Desea repetir el programa? (s/n): ";
		std::cin >> respuesta;

	} while (respuesta == 's' || respuesta == 'S');
}

