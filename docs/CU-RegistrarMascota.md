
|<!-- -->|<!-- -->|
|:-------------:|:---------------:|
|Caso de uso|Registrar mascota|
|Descripción|El usuario registra una mascota en la aplicación|
|Actores|Usuario|
|Pre condición|El usuario debe estar en la vista UserPets con el listado de sus mascotas si las tuviese.|
|Post condición|El usuario es devuelto a la vista UserPets listando la nueva mascota registrada.|
|Excepciones|El usuario no inició sesión. Los datos ingresados son incorrectos.|

```mermaid
sequenceDiagram
    actor user as Usuario
    participant view as UserPets
    participant form as FormAddPet
    participant controller as PetsController
    participant pets as TablePets
    participant petstutors as TablePetTutorsPets

    user->>view: Click en registrar mascota
    view->>controller: GET: FormAddPet()
    controller->>user: redirigir a FormAddPet
    user->>form: llenar formulario y aceptar
    form->>form: validar campos
    form->>controller: POST: FormAddPet(formData)
    controller->>controller: validar datos
    controller->>pets: add(pet)
    pets->>controller: petId
    controller->>petstutors: add(petId, userId)
    petstutors->>controller: ok
    controller->>user: redirigir a UserPets

```