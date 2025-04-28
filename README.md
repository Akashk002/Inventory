I designed and implemented a complete Inventory and Shop System utilizing the Service Locator, MVC, and Observer design patterns.

Players can purchase items from the Shop, which are seamlessly added to their Inventory, and sell items from their Inventory back to the Shop.

The system efficiently manages item transfers, and provides real-time updates to the player's coin balance and inventory weight, ensuring smooth and intuitive interactions between Shop and Inventory components.

The architecture is centered around a GameService, responsible for initializing and managing all core services, including:

EventService (handles event broadcasting and listening),

ShopService (manages shop-related logic and item transactions),

InventoryService (manages player's inventory state and operations),

AudioService (manages sound effects for inventory and shop actions).
Each service operates independently according to its responsibilities, ensuring a modular, maintainable, and scalable system structure.