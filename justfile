COMPOSE_APPS := "app migrations"
COMPOSE_DEPS := "postgres kafka pgweb"

##
# App Lifecycle Recipes
##

# Start the app services
apps-up:
    # Ensure the deps are up before the apps
    just deps-up
    just svc-up "{{COMPOSE_APPS}}"

# Stop the app services' containers
apps-stop:
    just svc-stop "{{COMPOSE_APPS}}"

# Re/start the app services' containers
apps-restart:
    # Ensure the deps are up before the apps
    just deps-up
    just svc-restart "{{COMPOSE_APPS}}"

# Recreate/rebuild the given services
apps-recreate:
    # Ensure the deps are up before the apps
    just deps-up
    just svc-recreate "{{COMPOSE_APPS}}"

# Stop and remove containers for the app services
apps-down:
    just svc-stop "{{COMPOSE_APPS}}"

##
# Dependency Lifecycle Recipes
##

# Start the dependency services
deps-up:
    just svc-up {{COMPOSE_DEPS}}

# Stop the dependency services' containers
deps-stop:
    just svc-stop {{COMPOSE_DEPS}}
    
# Re/start the dependency services' containers
deps-restart:
    just svc-restart {{COMPOSE_DEPS}}

# Recreate/rebuild the given services
deps-recreate:
    just svc-recreate {{COMPOSE_DEPS}}

# Stop and remove containers for the dependency services
deps-down:
    just svc-stop {{COMPOSE_DEPS}}

##
# Service Lifecycle Recipes
## 

# Start the given services (defaults to all)
svc-up +SERVICES="":
    docker compose up --detach {{SERVICES}}

# Stop the given services' containers (defaults to all)
svc-stop +SERVICES="":
    docker compose stop {{SERVICES}}

# Re/start the given services containers (defaults to all)
svc-restart +SERVICES="":
    # `docker compose restart` only restarts running services. This will stop
    # them if they're running then ensure they're started.
    just svc-stop {{SERVICES}}
    just svc-up {{SERVICES}}

# Recreate/rebuild the given services (defaults to all)
svc-recreate +SERVICES="":
    docker compose up --detach --build --force-recreate -V {{SERVICES}}

# Stop and remove containers for the given services (defaults to all)
svc-down +SERVICES="":
    docker compose down {{SERVICES}}

##
# Workflow Recipes
##

# Creates a new migration with the current DbContext changes
migrations-add NAME:
    dotnet ef migrations add --startup-project Things.Api --project Things.Database {{NAME}}

##
# Miscellaneous Recipes
##

open-swagger:
    2>/dev/null start http://localhost:8080/swagger

# Run pgweb and launch browser
open-pgweb:
    2>/dev/null start http://localhost:8432
