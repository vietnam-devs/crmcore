import React, { Component } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { Container } from 'reactstrap';

// components
import { Header, Sidebar, Breadcrumb, Aside, Footer } from 'components';

// containers
import {
  MetadataDashboard,
  FieldMgt,
  SchemaMgt,
  SchemaForm,
  Restricted
} from 'containers';

class MetadataLayout extends Component {
  render() {
    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <Sidebar {...this.props} />
          <main className="main">
            <Breadcrumb />
            <Container fluid>
              <Switch>
                <Route
                  path="/metadata/dashboard"
                  name="Metadata Dashboard"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(MetadataDashboard)}
                    </Restricted>
                  )}
                />
                <Route
                  path="/metadata/fields"
                  name="Fields"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(FieldMgt)}
                    </Restricted>
                  )}
                />
                <Route
                  exact
                  path="/metadata/schemas"
                  name="Schemas"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(SchemaMgt)}
                    </Restricted>
                  )}
                />
                <Route
                  exact
                  path="/metadata/schema-form/:name"
                  name="SchemaForm"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(SchemaForm)}
                    </Restricted>
                  )}
                />
                <Redirect from="/metadata" to="/metadata/dashboard" />
              </Switch>
            </Container>
          </main>
          <Aside />
        </div>
        <Footer />
      </div>
    );
  }
}

export default MetadataLayout;
