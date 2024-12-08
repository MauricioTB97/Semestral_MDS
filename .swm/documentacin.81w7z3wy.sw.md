---
title: Documentación
---
&nbsp;

&nbsp;

En este documento se encuadra la documentación pertinente de los Scripts realizados para la creación del videojuego.

# Código Enemigo

<SwmSnippet path="/Assets/Script/Enemy.cs" line="49">

---

Aquí movemos el enemigo hacia una posición objetivo en función de un conjunto de puntos de referencia. Si el objeto alcanza la posición objetivo, actualizamos el índice del waypoint y cambiamos la dirección del movimiento si es necesario.

```c#
    private void Move()
    {
        if (waypointIndex >= 0 && waypointIndex < waypoints.Length)
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Comparación de posiciones utilizando una pequeña tolerancia para manejar las diferencias mínimas
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                if (movingForward)
                {
                    waypointIndex++;
                    if (waypointIndex >= waypoints.Length)
                    {
                        movingForward = false;
                        waypointIndex = waypoints.Length - 2;
                        // Aquí podrías querer cambiar la velocidad de movimiento si necesitas un ajuste de comportamiento más suave
                        // moveSpeed *= -1; // Invertir la dirección del movimiento (si es necesario)
                        FlipSprite();
                    }
                }
                else
                {
                    waypointIndex--;
                    if (waypointIndex < 0)
                    {
                        movingForward = true;
                        waypointIndex = 1;
                        // Aquí también podrías querer cambiar la velocidad de movimiento si necesitas un ajuste más suave
                        // moveSpeed *= -1; // Invertir la dirección del movimiento (si es necesario)
                        FlipSprite();
                    }
                }
            }
        }
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Enemy.cs" line="87">

---

Aquí  se muestra cómo voltear un Sprite en C#

```c#
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Enemy.cs" line="94">

---

Este método  se llama cuando un objeto entra en contacto con el objeto actual y se encarga de gestionar las colisiones  con otros objetos  en el juego  y ejecutar las acciones correspondientes  como por ejemplo, la animación de daño del jugador  y la disminución de vidas del jugador.

```c#

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("daño");
            Player player = collision.GetComponent<Player>();
            player.animator.SetTrigger("IsHurt");
            GameController.Instance.LoseLives();
        }
        if (collision.CompareTag("pew"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
```

---

</SwmSnippet>

# Código Cherrys (Curas)

<SwmSnippet path="/Assets/Script/Cherrys.cs." line="16">

---

Aquí comprobamos si el otro objeto tiene una etiqueta llamada Jugador. Si es así, registramos 'vida' y llamamos a una función para agregar vidas usando <SwmToken path="/Assets/Script/Cherrys.cs." pos="21:7:9" line-data="            bool liverestore = GameController.Instance.AddLives();">`GameController.Instance`</SwmToken>. Si se agregan vidas con éxito, destruimos el objeto del juego actual.

```text
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("vida");
            bool liverestore = GameController.Instance.AddLives();
            if (liverestore)
            {
                Destroy(this.gameObject);
            }
        }
    }
```

---

</SwmSnippet>

# Código Seguimiento Enemigo

<SwmSnippet path="/Assets/Script/EnemyFollow.cs" line="17">

---

Este método actualiza la posición del jugador en el juego  cada vez que se llama  a la función <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="17:3:5" line-data="    void Update()">`Update()`</SwmToken> y llama a la función <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="20:1:3" line-data="        seguirjugador();">`seguirjugador()`</SwmToken> para seguir al jugador en el juego.

```c#
    void Update()

    {
        seguirjugador();
        tiempopasado += Time.deltaTime;
        if (tiempopasado >= tiempocambiodireccion)
        {

            cambiardireccionaleatoria();
            tiempopasado = 0f;
        }
    }

```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/EnemyFollow.cs" line="30">

---

Aquí comprobamos si jugador no es nulo. Si no es nulo, calculamos la dirección normalizada desde <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="34:8:10" line-data="            Vector2 direccionaljugador = (jugador.position - transform.position).normalized;">`jugador.position`</SwmToken> hasta <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="34:14:16" line-data="            Vector2 direccionaljugador = (jugador.position - transform.position).normalized;">`transform.position`</SwmToken> y la almacenamos en direccionaljugador. Luego calculamos el vector de movimiento multiplicando el direccionaljugador por la velocidad, <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="35:30:32" line-data="            Vector3 movimiento = new Vector3(direccionaljugador.x, direccionaljugador.y, 0) * velocidad * Time.deltaTime;">`Time.deltaTime`</SwmToken> y <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="35:1:1" line-data="            Vector3 movimiento = new Vector3(direccionaljugador.x, direccionaljugador.y, 0) * velocidad * Time.deltaTime;">`Vector3`</SwmToken>(0, 0, 0). Finalmente, traducimos la transformación por movimiento.

```c#
    void seguirjugador()
    {
        if (jugador != null)
        {
            Vector2 direccionaljugador = (jugador.position - transform.position).normalized;
            Vector3 movimiento = new Vector3(direccionaljugador.x, direccionaljugador.y, 0) * velocidad * Time.deltaTime;
            transform.Translate(movimiento);
        }

    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/EnemyFollow.cs" line="41">

---

Aquí generamos una dirección aleatoria seleccionando valores aleatorios para randomx y randomy entre -1 y 1, y luego creamos un <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="46:1:1" line-data="        Vector2 direccionaleatoria = new Vector2(randomx, randomy).normalized;">`Vector2`</SwmToken> normalizado llamado direccionaleatoria.

```c#
    void cambiardireccionaleatoria()
    {

        float randomx = Random.RandomRange(-1f, 1f);
        float randomy = Random.RandomRange(-1f, 1f);
        Vector2 direccionaleatoria = new Vector2(randomx, randomy).normalized;

    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/EnemyFollow.cs" line="49">

---

Este método se encarga de detectar colisiones y aplicar daño al jugador cuando entra en contacto con un enemigo o un proyectil en el juego.  Para aplicar daño al jugador, se llama a la función <SwmToken path="/Assets/Script/EnemyFollow.cs" pos="56:5:7" line-data="            GameController.Instance.LoseLives();">`LoseLives()`</SwmToken> del controlador del juego.

```c#
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("daño");
            Player player = collision.GetComponent<Player>();
            player.animator.SetTrigger("IsHurt");
            GameController.Instance.LoseLives();
            Destroy(gameObject);
        }
        if (collision.CompareTag("pew"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
```

---

</SwmSnippet>

# Código GameController&nbsp;

<SwmSnippet path="/Assets/Script/GameController.cs" line="26">

---

Este método se encarga de inicializar el <SwmToken path="/Assets/Script/GameController.cs" pos="34:15:15" line-data="            Debug.Log(&quot;Cuidado! Mas de un GameManager en escena.&quot;);">`GameManager`</SwmToken> al comienzo de la escena  y asegura que solo haya una instancia del mismo en la escena

```c#
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/GameController.cs" line="38">

---

El método <SwmToken path="/Assets/Script/GameController.cs" pos="38:5:7" line-data="    public void GameOver()">`GameOver()`</SwmToken> se encarga de reiniciar el juego  al terminar la partida  y mostrar la pantalla de derrota al jugador.

```c#
    public void GameOver()
    {
        Debug.Log("Game Over");
        perdiste.enabled = true;
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");

    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/GameController.cs" line="50">

---

Aquí agregamos los puntos a sumar al <SwmToken path="/Assets/Script/GameController.cs" pos="52:1:1" line-data="        TotalPoint += puntosasumar;">`TotalPoint`</SwmToken> y actualizamos los puntos en el hud.

```c#
    public void AddPoints(int puntosasumar)
    {
        TotalPoint += puntosasumar;
        hud.UpdatePoints(TotalPoint);
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/GameController.cs" line="55">

---

Aquí disminuimos la variable Lives en 1. Si Lives es menor o igual a 0, llamamos a la función <SwmToken path="/Assets/Script/GameController.cs" pos="60:1:1" line-data="            GameOver();">`GameOver`</SwmToken>. Finalmente, actualizamos la visualización de Lives en el HUD.

```c#
    public void LoseLives()
    {
        Lives -= 1;
        if (Lives <= 0)
        {
            GameOver();
        }
        hud.LivesOff(Lives);
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/GameController.cs" line="64">

---

Aquí tenemos una función <SwmToken path="/Assets/Script/GameController.cs" pos="64:5:5" line-data="    public bool AddLives()">`AddLives`</SwmToken> que agrega una vida si el número actual de vidas es menor que 3 y devuelve verdadero. Si el número actual de vidas ya es 3 o más, devuelve falso. Además, llama a la función Liveson del objeto hud y pasa el número actual de vidas como argumento.

```c#
    public bool AddLives()
    {
        if (Lives >= 3)
        {
            return false;
        }
        hud.Liveson(Lives);
        Lives += 1;
        return true;
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/GameController.cs" line="75">

---

Este  método se llama cuando ganas el juego  y muestra un mensaje de "Game Over".

```c#
    public void Ganaste()
    {
        Debug.Log("Game Over");
        ganaste.enabled = true;
        StartCoroutine(RestartGame());
    }
```

---

</SwmSnippet>

# Código Gems (Moneda)

<SwmSnippet path="/Assets/Script/Gems.cs" line="22">

---

Aquí, cuando el jugador choca con el objeto, el código agrega puntos al juego, activa una animación, registra un mensaje y destruye el objeto.  El código también obtiene el componente Animator del jugador y llama a la función <SwmToken path="/Assets/Script/Gems.cs" pos="29:3:4" line-data="            gc.AddPoints(valor);">`AddPoints(`</SwmToken> del controlador del juego para agregar puntos.

```c#
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //animator.SetTrigger("Fx");
            Debug.Log("puntos añadidos");
            animator = collision.GetComponent<Animator>();
            gc.AddPoints(valor);
            Destroy(this.gameObject);
        }

    }
```

---

</SwmSnippet>

# Código del HUD

<SwmSnippet path="/Assets/Script/HUD.cs" line="17">

---

Aquí actualizamos el texto de Gems para mostrar los puntos totales de la instancia de <SwmToken path="/Assets/Script/HUD.cs" pos="19:7:7" line-data="        Gems.text = GameController.Instance.TotalPoint.ToString();">`GameController`</SwmToken>.

```c#
    void Update()
    {
        Gems.text = GameController.Instance.TotalPoint.ToString();
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/HUD.cs" line="22">

---

Aquí actualizamos el texto de Gems para mostrar el valor de punto s totales como una cadena.

```c#
    public void UpdatePoints(int puntostotales)
    {
        Gems.text = puntostotales.ToString();
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/HUD.cs" line="26">

---

Aquí desactivamos un elemento específico en la matriz de vidas.&nbsp;

```c#
    public void LivesOff(int index)
    {
        lives[index].SetActive(false);
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/HUD.cs" line="30">

---

Aquí activamos un elemento de la lista de vidas

```c#
    public void Liveson(int index)
    {
        lives[index].SetActive(true);
    }

```

---

</SwmSnippet>

# Código Menú Principal

<SwmSnippet path="/Assets/Script/MainMenu.cs" line="20">

---

Este método inicia el juego  cargando la escena <SwmToken path="/Assets/Script/MainMenu.cs" pos="22:6:6" line-data="        SceneManager.LoadScene(&quot;Game&quot;);">`Game`</SwmToken>

```c#
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/MainMenu.cs" line="25">

---

Este  método es utilizado para cerrar la aplicación.

```c#
    public void Exit() 
    {
        Application.Quit();
    }
```

---

</SwmSnippet>

# Código Jugador

<SwmSnippet path="/Assets/Script/Player.cs" line="25">

---

Aquí inicializamos las variables rb, animator y <SwmToken path="/Assets/Script/Player.cs" pos="29:1:1" line-data="        boxCollider = GetComponent&lt;CapsuleCollider2D&gt;();">`boxCollider`</SwmToken> con los componentes correspondientes. También configuramos la variable <SwmToken path="/Assets/Script/Player.cs" pos="30:1:1" line-data="        JumpsRemaining = MaxJumps;">`JumpsRemaining`</SwmToken> al número máximo de saltos.

```c#
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        JumpsRemaining = MaxJumps;
    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Player.cs" line="33">

---

Aquí actualizamos el estado del juego según la propiedad Lives del <SwmToken path="/Assets/Script/Player.cs" pos="35:3:3" line-data="        GameController gameController = GameObject.Find(&quot;GameController&quot;).GetComponent&lt;GameController&gt;();">`gameController`</SwmToken>. Si las vidas son mayores que 0, nos movemos, saltamos y buscamos entradas para Disparar. De lo contrario, configuramos el parámetro del animador <SwmToken path="/Assets/Script/Player.cs" pos="48:6:6" line-data="            animator.SetBool(&quot;IsHurt&quot;, true);">`IsHurt`</SwmToken> en verdadero, detenemos el movimiento del objeto, iniciamos una rutina llamada Moricion, desactivamos el colisionador y destruimos el objeto después de 2 segundos. Actualizamos el estado del juego según la propiedad Lives del <SwmToken path="/Assets/Script/Player.cs" pos="35:3:3" line-data="        GameController gameController = GameObject.Find(&quot;GameController&quot;).GetComponent&lt;GameController&gt;();">`gameController`</SwmToken>.

```c#
    void Update()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (gameController.Lives > 0)
        {
            Move();
            Jump();
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFireTime)
            {
                Disparar();
            }

        }
        else
        {
            animator.SetBool("IsHurt", true);
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("IsRunning", false);
            StartCoroutine(Moricion());
            col.enabled = false;
            Destroy(gameObject, 2);
        }
    }

```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Player.cs" line="58">

---

Aquí controlamos el movimiento de un objeto según la entrada del teclado. Si se presiona la tecla 'A', el objeto se mueve hacia la izquierda, actualiza su animación y cambia su escala. Si se presiona la tecla 'D', el objeto se mueve hacia la derecha, actualiza su animación y mantiene su escala. Si no se presiona ninguna tecla de movimiento, el objeto deja de moverse y actualiza su animación en consecuencia.  Para mover un objeto según la entrada del teclado, use la función Move() en la función Update()

```c#
    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetBool("IsRunning", true);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetBool("IsRunning", true);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("IsRunning", false);
        }
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Player.cs" line="80">

---

Este método es utilizado para disparar proyectiles desde la posición del objeto que lo llama

```c#
    void Disparar()
    {
        nextFireTime = Time.time + fireRate;
        Vector3 direccion = transform.localScale.x > 0 ? Vector3.right : Vector3.left;

        GameObject bala = Instantiate(proyectil, transform.position, Quaternion.identity);
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
        rbBala.velocity = direccion * 10f; 
        if (direccion == Vector3.left)
        {
            Vector3 escala = bala.transform.localScale;
            escala.x *= -1;
            bala.transform.localScale = escala;
        }


    }
```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Player.cs" line="97">

---

Aquí tenemos una función de Salto que permite al personaje saltar si está castigado y le quedan saltos restantes. Comprueba si el personaje está conectado a tierra y establece <SwmToken path="/Assets/Script/Player.cs" pos="30:1:1" line-data="        JumpsRemaining = MaxJumps;">`JumpsRemaining`</SwmToken> en el número máximo de saltos. Si se presiona la tecla de salto y quedan saltos, se activa la animación del salto, disminuye el recuento de saltos restantes, restablece la velocidad Y y aplica una fuerza hacia arriba al cuerpo rígido.

```c#
    void Jump()
    {
        if (IsGrounded())
        {
            JumpsRemaining = MaxJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpsRemaining > 0)
        {
            animator.SetTrigger("IsJumping");
            JumpsRemaining--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x - 0.9f, boxCollider.bounds.size.y), 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;

    }

```

---

</SwmSnippet>

<SwmSnippet path="/Assets/Script/Player.cs" line="118">

---

Aquí manejamos eventos de colisión en un entorno 2D. Si ocurre una colisión con la etiqueta <SwmToken path="/Assets/Script/Player.cs" pos="120:12:14" line-data="        if (collision.tag == (&quot;Perk-Jump&quot;))">`Perk-Jump`</SwmToken>, incrementamos la variable <SwmToken path="/Assets/Script/Player.cs" pos="122:1:1" line-data="            MaxJumps++;">`MaxJumps`</SwmToken> y destruimos el objeto colisionado. Si ocurre una colisión con la clave de etiqueta, registramos un mensaje, llamamos a la función Ganaste y destruimos el objeto colisionado. Para colisiones con etiquetas <SwmToken path="/Assets/Script/Player.cs" pos="131:11:11" line-data="        if (collision.tag == &quot;Activador1&quot;)">`Activador1`</SwmToken> a <SwmToken path="/Assets/Script/Player.cs" pos="173:11:11" line-data="        if (collision.tag == &quot;Activador8&quot;)">`Activador8`</SwmToken>, creamos una instancia de un objeto enemigo volador en posiciones específicas y destruimos el objeto colisionado. Por último, existe una corrutina Moricion que cambia la velocidad de rb durante un corto período de tiempo.

```c#
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Perk-Jump"))
        {
            MaxJumps++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "key")
        {
            Debug.Log("Pegatis");
            gc.Ganaste();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Activador1")
        {
            Instantiate(flyenemy, new Vector3(218, -39, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador2")
        {
            Instantiate(flyenemy, new Vector3(254, -7, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador3")
        {
            Instantiate(flyenemy, new Vector3(314, 22, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador4")
        {
            Instantiate(flyenemy, new Vector3(375, 28, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador5")
        {
            Instantiate(flyenemy, new Vector3(442, 48, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador6")
        {
            Instantiate(flyenemy, new Vector3(475, 93, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador7")
        {
            Instantiate(flyenemy, new Vector3(528, 95, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador8")
        {
            Instantiate(flyenemy, new Vector3(665, -93, 0), Quaternion.identity);

        }
    }

    IEnumerator Moricion()
    {
        rb.velocity = new Vector2(0, 10);
        yield return new WaitForSeconds(0.3f);

        rb.velocity = new Vector2(0, -10);
    }
```

---

</SwmSnippet>

# Código Spikes

<SwmSnippet path="/Assets/Script/Spikes.cs" line="18">

---

Aquí manejamos el evento cuando un objeto <SwmToken path="/Assets/Script/Spikes.cs" pos="18:5:5" line-data="    void OnTriggerEnter2D(Collider2D collision)">`Collider2D`</SwmToken> ingresa al disparador. Si el objeto en colisión tiene la etiqueta 'Jugador', registramos 'daño'. Luego, obtenemos el componente Player del objeto en colisión y activamos la animación <SwmToken path="/Assets/Script/Spikes.cs" pos="24:8:8" line-data="            player.animator.SetTrigger(&quot;IsHurt&quot;);">`IsHurt`</SwmToken>. Finalmente, llamamos a la función <SwmToken path="/Assets/Script/Spikes.cs" pos="25:5:5" line-data="            GameController.Instance.LoseLives();">`LoseLives`</SwmToken> desde la instancia de <SwmToken path="/Assets/Script/HUD.cs" pos="19:7:7" line-data="        Gems.text = GameController.Instance.TotalPoint.ToString();">`GameController`</SwmToken>.

```c#
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("daño");
            Player player = collision.GetComponent<Player>();
            player.animator.SetTrigger("IsHurt");
            GameController.Instance.LoseLives();
        }
    }
```

---

</SwmSnippet>

<SwmMeta version="3.0.0" repo-id="Z2l0aHViJTNBJTNBU2VtZXN0cmFsX01EUyUzQSUzQU1hdXJpY2lvVEI5Nw==" repo-name="Semestral_MDS"><sup>Powered by [Swimm](https://app.swimm.io/)</sup></SwmMeta>
