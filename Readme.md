# Spirv assembler for SDSL


## Main goal 

Create a spirv assembler for SDSL where shader bits and pieces are written in separate files as mixins and assembled through the compiler.



## Basic design

First step is to generate spirv byte code for each mixins. Each mixins will contain functions, variables, types and parameters/bindings.

Language : 

1. Since a mixin can override 