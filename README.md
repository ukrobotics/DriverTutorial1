# Revolution Driver Tutorial

## Intro
This repo contains examples to aid 3rd parties to create their own drivers or plugins for Revolution.

In Revolution, a driver ( also refered to as a device ) is a C# class that is a plugin to Revolution. 
A driver can control hardware but it can also simply be a plugin that can be called from a schedule operation to do non-hardware related
actions, EG reading or writing to a DB.  You can think of a driver as a plugin that can control hardware, or do anything in fact. 
It can be as simple as an empty class definition, or as complex as a plugin for a liquid handler backed by a LIMS/DB interface.


